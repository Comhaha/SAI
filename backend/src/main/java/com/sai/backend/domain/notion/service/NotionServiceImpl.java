package com.sai.backend.domain.notion.service;

import com.sai.backend.domain.ai.model.AiLog;
import com.sai.backend.domain.ai.repository.mongo.AiFeedbackRepository;
import com.sai.backend.domain.notion.dto.request.CallbackRequestDto;
import com.sai.backend.domain.notion.dto.response.ExportResponseDto;
import com.sai.backend.domain.notion.model.NotionAccessToken;
import com.sai.backend.domain.notion.model.NotionStateToken;
import com.sai.backend.domain.notion.repository.redis.NotionStateRepository;
import com.sai.backend.domain.notion.repository.redis.NotionTokenRepository;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Service;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;
import org.springframework.web.reactive.function.BodyInserters;
import org.springframework.web.reactive.function.client.WebClient;
import org.springframework.web.reactive.function.client.WebClientResponseException;
import reactor.core.publisher.Mono;

import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;
import java.time.LocalDateTime;
import java.util.Base64;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

@Service
@RequiredArgsConstructor
@Slf4j
public class NotionServiceImpl implements NotionService {

    private final AiFeedbackRepository aiFeedbackRepository;
    private final NotionStateRepository notionStateRepository;
    private final NotionTokenRepository notionTokenRepository;
    private final WebClient notionWebClient;
    private final NotionExportService notionExportService;

    @Value("${notion.client.id}")
    private String clientId;

    @Value("${notion.client.secret}")
    private String clientSecret;

    @Value("${notion.redirect.uri}")
    private String redirectUri;

    @Override
    public String generateAuthUrl(String uuid) {
        log.info("Generating auth URL for UUID: {}", uuid);

        // AiLog 확인
        AiLog ailog = aiFeedbackRepository.findById(uuid).orElse(null);
        if (ailog == null) {
            log.warn("AiLog not found for UUID: {}", uuid);
            return null;
        }

        // 기존 토큰이 있다면 삭제 (새로운 토큰 발급을 위해)
        NotionAccessToken existingToken = notionTokenRepository.findByUuid(uuid).orElse(null);
        if (existingToken != null) {
            log.info("Deleting existing token for UUID: {} to issue new token", uuid);
            notionTokenRepository.delete(existingToken);
        }

        // 기존 state 토큰이 있다면 삭제
        NotionStateToken existingState = notionStateRepository.findByUuid(uuid).orElse(null);
        if (existingState != null) {
            log.info("Deleting existing state token for UUID: {}", uuid);
            notionStateRepository.delete(existingState);
        }

        // 새로운 state 생성
        String state = UUID.randomUUID().toString();
        NotionStateToken stateToken = NotionStateToken.create(state, uuid, redirectUri);
        notionStateRepository.save(stateToken);
        log.info("Created new state token: {} for UUID: {}", state, uuid);

        // 인증 URL 생성
        String authUrl = String.format(
            "https://api.notion.com/v1/oauth/authorize?client_id=%s&response_type=code&owner=user&redirect_uri=%s&state=%s",
            clientId,
            URLEncoder.encode(redirectUri, StandardCharsets.UTF_8),
            state
        );

        return authUrl;
    }

    @Override
    public ExportResponseDto handleOAuthCallback(CallbackRequestDto dto) {
        log.info("=== OAuth Callback 시작 ===");
        log.info("Received - code: {}, state: {}", dto.getCode(), dto.getState());

        // State 토큰 조회
        NotionStateToken stateToken = notionStateRepository.findByState(dto.getState()).orElse(null);

        if (stateToken == null) {
            log.error("State token not found. State: {}", dto.getState());
            return ExportResponseDto.builder()
                .success(false)
                .message("Invalid state token")
                .build();
        }

        String uuid = stateToken.getUuid();
        log.info("State token found - UUID: {}", uuid);

        try {
            // 코드를 액세스 토큰으로 교환
            String auth = clientId + ":" + clientSecret;
            String encodedAuth = Base64.getEncoder().encodeToString(auth.getBytes());

            MultiValueMap<String, String> body = new LinkedMultiValueMap<>();
            body.add("grant_type", "authorization_code");
            body.add("code", dto.getCode());
            body.add("redirect_uri", redirectUri);

            log.info("Exchanging code for token...");

            Map<String, Object> response = notionWebClient
                .post()
                .uri("/v1/oauth/token")
                .header("Authorization", "Basic " + encodedAuth)
                .contentType(MediaType.APPLICATION_FORM_URLENCODED)
                .body(BodyInserters.fromFormData(body))
                .retrieve()
                .onStatus(httpStatus -> httpStatus.value() >= 400, clientResponse -> {
                    return clientResponse.bodyToMono(String.class)
                        .doOnNext(errorBody -> log.error("OAuth 토큰 교환 실패 - Status: {}, Body: {}",
                            clientResponse.statusCode(), errorBody))
                        .then(Mono.error(new RuntimeException("OAuth 토큰 교환 실패: " + clientResponse.statusCode())));
                })
                .bodyToMono(new ParameterizedTypeReference<Map<String, Object>>() {})
                .block();

            // 액세스 토큰 저장
            String accessToken = (String) response.get("access_token");
            log.info("Access token received, storing for UUID: {}", uuid);

            // OAuth 응답에서 워크스페이스 정보 추출
            log.info("OAuth 전체 응답: {}", response);
            String workspaceId = extractWorkspaceInfo(response, uuid, accessToken);
            log.info("추출된 워크스페이스 정보: workspaceId={}", workspaceId);

            // 토큰 저장
            NotionAccessToken.NotionAccessTokenBuilder tokenBuilder = NotionAccessToken.builder()
                .uuid(uuid)
                .response(response)
                .accessToken(accessToken)
                .createdAt(LocalDateTime.now())
                .expiresAt(LocalDateTime.now().plusDays(30))
                .ttl(2592000L);

            // OAuth 응답에서 duplicated_template_id 및 workspace_id 저장
            if (response.containsKey("duplicated_template_id")) {
                String duplicatedTemplateId = (String) response.get("duplicated_template_id");
                tokenBuilder.duplicatedTemplateId(duplicatedTemplateId);
                log.info("Duplicated Template ID 저장: {}", duplicatedTemplateId);
            }

            if (workspaceId != null) {
                tokenBuilder.workspaceId(workspaceId);
                log.info("Workspace ID 저장: {}", workspaceId);
            }

            NotionAccessToken notionToken = tokenBuilder.build();
            notionTokenRepository.save(notionToken);

            // 상태 토큰 삭제
            notionStateRepository.delete(stateToken);
            log.info("State token deleted: {}", dto.getState());

            // AiLog 조회하여 export 처리
            AiLog aiLog = aiFeedbackRepository.findById(uuid).orElse(null);
            if (aiLog != null) {
                return processExportAndReturnResult(aiLog, notionToken);
            } else {
                return ExportResponseDto.builder()
                    .success(false)
                    .message("AI 로그를 찾을 수 없습니다.")
                    .build();
            }

        } catch (Exception e) {
            log.error("OAuth callback error", e);
            return ExportResponseDto.builder()
                .success(false)
                .message("OAuth 처리 중 오류가 발생했습니다: " + e.getMessage())
                .build();
        }
    }

    /**
     * Export 처리 후 ExportResponseDto 반환
     */
    private ExportResponseDto processExportAndReturnResult(AiLog aiLog, NotionAccessToken token) {
        try {
            // Export 처리 로직 (기존 exportToNotion 메서드 내용 활용)
            String workspaceInfo = findObjectTypeViaSearch(token.getAccessToken(), token.getUuid());
            ExportResponseDto exportResult = null;

            if (workspaceInfo != null) {
                log.info("workspcaceInfo: {} 찾음", workspaceInfo);
                String[] parts = workspaceInfo.split(":");
                if (parts.length == 2) {
                    String objectType = parts[0];
                    String targetObjectId = parts[1];

                    if ("database".equals(objectType)) {
                        exportResult = notionExportService.exportToDatabasePage(aiLog, token.getAccessToken(), targetObjectId);
                    } else if ("page".equals(objectType)) {
                        exportResult = notionExportService.exportToPageChild(aiLog, token.getAccessToken(), targetObjectId);
                    }
                }
            } else {
                //기본 처리
                log.info("No workspace found for UUID: {} 못 찾음", token.getUuid());
                String defaultPageId = notionExportService.findDefaultPageForExport(token.getAccessToken());
                if (defaultPageId != null) {
                    exportResult = notionExportService.exportToPageChild(aiLog, token.getAccessToken(), defaultPageId);
                } else {
                    exportResult = notionExportService.createStandalonePageFallback(aiLog, token.getAccessToken());
                }
            }

            // Export 결과 반환
            if (exportResult != null && exportResult.getSuccess()) {
                log.info("Export 성공, 생성된 페이지 URL: {}", exportResult.getUrl());
                return exportResult;
            } else {
                log.warn("Export 실패");
                return ExportResponseDto.builder()
                    .success(false)
                    .message("Notion 내보내기에 실패했습니다.")
                    .build();
            }

        } catch (Exception e) {
            log.error("Export 처리 실패", e);
            return ExportResponseDto.builder()
                .success(false)
                .message("Export 처리 중 오류가 발생했습니다: " + e.getMessage())
                .build();
        }
    }

    /**
     * OAuth 응답에서 워크스페이스 관련 정보 추출
     */
    private String extractWorkspaceInfo(Map<String, Object> oauthResponse, String uuid, String accessToken) {
        String workspaceId = null;

        try {
            log.info("OAuth 응답 파싱 시작...");

            // 1. OAuth 응답에서 workspace_id 직접 확인
            if (oauthResponse.containsKey("workspace_id")) {
                workspaceId = (String) oauthResponse.get("workspace_id");
                log.info("OAuth 응답에서 workspace_id 발견: {}", workspaceId);
            }

            // 2. duplicated_template_id 확인 (사용자가 템플릿을 선택한 경우)
            if (workspaceId == null && oauthResponse.containsKey("duplicated_template_id")) {
                workspaceId = (String) oauthResponse.get("duplicated_template_id");
                log.info("OAuth 응답에서 duplicated_template_id 발견: {}", workspaceId);
            }

            // 3. owner 정보에서 확인
            if (workspaceId == null && oauthResponse.containsKey("owner")) {
                Map<String, Object> owner = (Map<String, Object>) oauthResponse.get("owner");
                log.info("Owner 정보: {}", owner);

                if (owner.containsKey("workspace") && owner.get("workspace") instanceof Map) {
                    Map<String, Object> workspace = (Map<String, Object>) owner.get("workspace");
                    if (workspace.containsKey("id")) {
                        workspaceId = (String) workspace.get("id");
                        log.info("Owner.workspace에서 워크스페이스 ID 발견: {}", workspaceId);
                    }
                }
            }

            // 4. 추가적인 정보 로깅
            logAdditionalTokenInfo(oauthResponse);

        } catch (Exception e) {
            log.error("워크스페이스 정보 추출 중 오류", e);
        }

        return workspaceId;
    }

    /**
     * OAuth 응답의 추가 정보 로깅
     */
    private void logAdditionalTokenInfo(Map<String, Object> oauthResponse) {
        log.info("=== OAuth 응답 상세 분석 ===");

        // bot_id 정보
        if (oauthResponse.containsKey("bot_id")) {
            log.info("Bot ID: {}", oauthResponse.get("bot_id"));
        }

        // owner 정보 상세 로깅
        if (oauthResponse.containsKey("owner")) {
            Map<String, Object> owner = (Map<String, Object>) oauthResponse.get("owner");
            log.info("Owner 타입: {}", owner.get("type"));
            log.info("Owner 전체 정보: {}", owner);
        }

        // workspace 정보
        if (oauthResponse.containsKey("workspace_name")) {
            log.info("Workspace 이름: {}", oauthResponse.get("workspace_name"));
        }

        // duplicated_template_id
        if (oauthResponse.containsKey("duplicated_template_id")) {
            log.info("Duplicated Template ID: {}", oauthResponse.get("duplicated_template_id"));
        }

        // 기타 메타데이터
        log.info("OAuth 응답의 모든 키: {}", oauthResponse.keySet());
    }

    /**
     * /v1/search API를 통해 유저가 선택한 객체의 타입 확인
     */
    private String findObjectTypeViaSearch(String accessToken, String uuid) {
        try {
            log.info("Search API를 통한 객체 타입 확인 시작");

            // 검색 요청 생성 - 빈 쿼리로 모든 객체 검색
            Map<String, Object> searchRequest = new HashMap<>();
            searchRequest.put("query", ""); // 빈 쿼리로 모든 객체 검색
            searchRequest.put("page_size", 100); // 최대 100개까지 가져오기

            Map<String, Object> searchResponse = notionWebClient
                .post()
                .uri("/v1/search")
                .header("Authorization", "Bearer " + accessToken)
                .header("Notion-Version", "2022-06-28")
                .contentType(MediaType.APPLICATION_JSON)
                .bodyValue(searchRequest)
                .retrieve()
                .onStatus(httpStatus -> httpStatus.value() >= 400, clientResponse -> {
                    return clientResponse.bodyToMono(String.class)
                        .doOnNext(errorBody -> log.error("Search API 에러 - Status: {}, Body: {}",
                            clientResponse.statusCode(), errorBody))
                        .then(Mono.error(new RuntimeException("Search API 실패: " + clientResponse.statusCode())));
                })
                .bodyToMono(new ParameterizedTypeReference<Map<String, Object>>() {})
                .block();

            if (searchResponse != null && searchResponse.containsKey("results")) {
                List<Map<String, Object>> results = (List<Map<String, Object>>) searchResponse.get("results");
                log.info("검색 결과 개수: {}", results.size());

                // OAuth 응답에서 저장된 정보를 기반으로 일치하는 객체 찾기
                return findMatchingObject(results, uuid);
            }

        } catch (WebClientResponseException e) {
            log.error("Search API WebClient 에러: status={}, body={}",
                e.getStatusCode(), e.getResponseBodyAsString());
        } catch (Exception e) {
            log.error("Search API를 통한 객체 타입 확인 실패", e);
        }

        return null;
    }

    /**
     * 검색 결과에서 일치하는 객체 찾기
     */
    private String findMatchingObject(List<Map<String, Object>> searchResults, String uuid) {
        // OAuth 응답에서 저장된 정보를 가져오기
        NotionAccessToken token = notionTokenRepository.findByUuid(uuid).orElse(null);
        if (token == null) {
            log.warn("토큰을 찾을 수 없음: {}", uuid);
            return null;
        }

        // 저장된 ID들 확인
        String duplicatedTemplateId = token.getDuplicatedTemplateId();
        String workspaceId = token.getWorkspaceId();

        log.info("찾을 ID 목록 - duplicatedTemplateId: {}, workspaceId: {}", duplicatedTemplateId, workspaceId);

        for (Map<String, Object> result : searchResults) {
            String id = (String) result.get("id");
            String object = (String) result.get("object");

            log.debug("검사 중인 객체: ID={}, Type={}", id, object);

            // ID가 일치하는 객체 찾기
            boolean isMatch = false;
            if (duplicatedTemplateId != null && duplicatedTemplateId.equals(id)) {
                isMatch = true;
                log.info("duplicatedTemplateId와 일치하는 객체 발견: ID={}", id);
            } else if (workspaceId != null && workspaceId.equals(id)) {
                isMatch = true;
                log.info("workspaceId와 일치하는 객체 발견: ID={}", id);
            }

            if (isMatch) {
                // 객체의 타입 확인
                if ("database".equals(object)) {
                    log.info("데이터베이스 타입 객체 확인됨: {}", id);
                    return "database:" + id;
                } else if ("page".equals(object)) {
                    log.info("페이지 타입 객체 확인됨: {}", id);
                    return "page:" + id;
                } else {
                    log.warn("알 수 없는 객체 타입: {} (ID: {})", object, id);
                }
            }
        }

        log.warn("일치하는 객체를 찾을 수 없음 - UUID: {}", uuid);
        return null;
    }
}

//리펙토링

//    @Override
//    public AuthCheckResponseDto checkAuthStatus(String uuid) {
//        log.info("Checking auth status for UUID: {}", uuid);
//
//        // AiLog 확인
//        AiLog ailog = aiFeedbackRepository.findById(uuid).orElse(null);
//        if (ailog == null) {
//            log.warn("AiLog not found for UUID: {}", uuid);
//        }
//
//        // 기존 토큰 확인
//        NotionAccessToken existingToken = notionTokenRepository.findByUuid(uuid).orElse(null);
//
//        if (existingToken != null && !existingToken.isExpired()) {
//            // 이미 유효한 토큰이 있는 경우
//            log.info("Valid token found for UUID: {}, returning authenticated=true", uuid);
//            return AuthCheckResponseDto.builder()
//                .authenticated(true)
//                .build();
//        }
//
//        // 이미 진행 중인 state 토큰이 있는지 확인
//        NotionStateToken existingState = notionStateRepository.findByUuid(uuid).orElse(null);
//
//        String state;
//        if (existingState != null) {
//            // 기존 state 재사용
//            state = existingState.getState();
//            log.info("Reusing existing state token: {} for UUID: {}", state, uuid);
//        } else {
//            // 새로운 state 생성
//            state = UUID.randomUUID().toString();
//            NotionStateToken stateToken = NotionStateToken.create(state, uuid, redirectUri);
//            notionStateRepository.save(stateToken);
//            log.info("Created new state token: {} for UUID: {}", state, uuid);
//        }
//
//        // 인증 URL 생성
//        String authUrl = String.format(
//            "https://api.notion.com/v1/oauth/authorize?client_id=%s&response_type=code&owner=user&redirect_uri=%s&state=%s",
//            clientId,
//            URLEncoder.encode(redirectUri, StandardCharsets.UTF_8),
//            state
//        );
//
//        return AuthCheckResponseDto.builder()
//            .authenticated(false)
//            .authUrl(authUrl)
//            .build();
//    }

//@Override
//public ExportResponseDto exportToNotion(String uuid) {
//    try {
//        log.info("Starting export for UUID: {}", uuid);
//
//        // AiLog 조회
//        AiLog aiLog = aiFeedbackRepository.findById(uuid)
//            .orElseThrow(() -> new IllegalArgumentException("AI 로그를 찾을 수 없습니다: " + uuid));
//
//        // Notion 액세스 토큰 확인
//        NotionAccessToken token = notionTokenRepository.findByUuid(uuid).orElse(null);
//
//        if (token == null || token.isExpired()) {
//            log.warn("토큰이 없거나 만료됨. UUID: {}", uuid);
//            return ExportResponseDto.builder()
//                .success(false)
//                .message("Notion 인증이 필요합니다.")
//                .build();
//        }
//
//        // /v1/search를 통해 저장된 페이지 정보 확인
//        String workspaceInfo = findObjectTypeViaSearch(token.getAccessToken(), uuid);
//        log.info("검색 결과 워크스페이스 정보: {}", workspaceInfo);
//
//        String targetObjectId = null;
//        String objectType = null;
//
//        if (workspaceInfo != null) {
//            String[] parts = workspaceInfo.split(":");
//            if (parts.length == 2) {
//                objectType = parts[0]; // "database" 또는 "page"
//                targetObjectId = parts[1]; // 실제 ID
//                log.info("객체 타입: {}, 객체 ID: {}", objectType, targetObjectId);
//            }
//        }
//
//        // 객체 타입에 따라 다른 처리
//        if ("database".equals(objectType) && targetObjectId != null) {
//            log.info("데이터베이스에 페이지 추가");
//            return notionExportService.exportToDatabasePage(aiLog, token.getAccessToken(), targetObjectId);
//        } else if ("page".equals(objectType) && targetObjectId != null) {
//            log.info("페이지의 하위 페이지로 추가");
//            return notionExportService.exportToPageChild(aiLog, token.getAccessToken(), targetObjectId);
//        } else {
//            // 기본 처리: 사용자가 권한을 가진 첫 번째 페이지에 하위 페이지로 추가
//            log.info("기본 처리: 접근 가능한 첫 번째 페이지에 하위 페이지 추가");
//            String defaultPageId = notionExportService.findDefaultPageForExport(token.getAccessToken());
//
//            if (defaultPageId != null) {
//                return notionExportService.exportToPageChild(aiLog, token.getAccessToken(), defaultPageId);
//            } else {
//                // 마지막 수단: 새 페이지 생성
//                return notionExportService.createStandalonePageFallback(aiLog, token.getAccessToken());
//            }
//        }
//
//    } catch (Exception e) {
//        log.error("Notion 내보내기 실패: {}", uuid, e);
//        return ExportResponseDto.builder()
//            .success(false)
//            .message("내보내기 중 오류가 발생했습니다: " + e.getMessage())
//            .build();
//    }
//}

//    @Override
//    public String handleOAuthCallback(CallbackRequestDto dto) {
//        log.info("=== OAuth Callback 시작 ===");
//        log.info("Received - code: {}, state: {}", dto.getCode(), dto.getState());
//
//        // State 토큰 조회
//        NotionStateToken stateToken = notionStateRepository.findByState(dto.getState()).orElse(null);
//
//        // State 토큰이 없다면
//        if (stateToken == null) {
//            log.error("State token not found. State: {}", dto.getState());
//            throw new IllegalArgumentException("Invalid state token");
//        }
//
//        String uuid = stateToken.getUuid();
//        log.info("State token found - UUID: {}", uuid);
//
//        try {
//            // 코드를 액세스 토큰으로 교환
//            String auth = clientId + ":" + clientSecret;
//            String encodedAuth = Base64.getEncoder().encodeToString(auth.getBytes());
//
//            MultiValueMap<String, String> body = new LinkedMultiValueMap<>();
//            body.add("grant_type", "authorization_code");
//            body.add("code", dto.getCode());
//            body.add("redirect_uri", redirectUri);
//
//            log.info("Exchanging code for token...");
//
//            Map<String, Object> response = notionWebClient
//                .post()
//                .uri("/v1/oauth/token")  // 올바른 Notion OAuth 엔드포인트
//                .header("Authorization", "Basic " + encodedAuth)
//                .contentType(MediaType.APPLICATION_FORM_URLENCODED)
//                .body(BodyInserters.fromFormData(body))
//                .retrieve()
//                .onStatus(httpStatus -> httpStatus.value() >= 400, clientResponse -> {
//                    return clientResponse.bodyToMono(String.class)
//                        .doOnNext(errorBody -> log.error("OAuth 토큰 교환 실패 - Status: {}, Body: {}",
//                            clientResponse.statusCode(), errorBody))
//                        .then(Mono.error(new RuntimeException("OAuth 토큰 교환 실패: " + clientResponse.statusCode())));
//                })
//                .bodyToMono(new ParameterizedTypeReference<Map<String, Object>>() {})
//                .block();
//
//            // 액세스 토큰 저장
//            String accessToken = (String) response.get("access_token");
//            log.info("Access token received, storing for UUID: {}", uuid);
//
//            // OAuth 응답에서 워크스페이스 정보 추출
//            log.info("OAuth 전체 응답: {}", response);
//            String workspaceId = extractWorkspaceInfo(response, uuid, accessToken);
//            log.info("추출된 워크스페이스 정보: workspaceId={}", workspaceId);
//
//            // 토큰 저장 - workspaceId도 함께 저장
//            NotionAccessToken.NotionAccessTokenBuilder tokenBuilder = NotionAccessToken.builder()
//                .uuid(uuid)
//                .response(response)
//                .accessToken(accessToken)
//                .createdAt(LocalDateTime.now())
//                .expiresAt(LocalDateTime.now().plusDays(30))
//                .ttl(2592000L);
//
//            // OAuth 응답에서 duplicated_template_id 및 workspace_id 저장
//            if (response.containsKey("duplicated_template_id")) {
//                String duplicatedTemplateId = (String) response.get("duplicated_template_id");
//                tokenBuilder.duplicatedTemplateId(duplicatedTemplateId);
//                log.info("Duplicated Template ID 저장: {}", duplicatedTemplateId);
//            }
//
//            if (workspaceId != null) {
//                tokenBuilder.workspaceId(workspaceId);
//                log.info("Workspace ID 저장: {}", workspaceId);
//            }
//
//            NotionAccessToken notionToken = tokenBuilder.build();
//            notionTokenRepository.save(notionToken);
//
//            // 상태 토큰 삭제
//            notionStateRepository.delete(stateToken);
//            log.info("State token deleted: {}", dto.getState());
//
//            // AiLog 조회하여 export 처리
//            AiLog aiLog = aiFeedbackRepository.findById(uuid).orElse(null);
//            if (aiLog != null) {
//                String notionPageUrl = processExportAndGetUrl(aiLog, notionToken);
//
//                return generateSuccessPage(notionPageUrl);
//            } else {
//                return generateSuccessPage(null);
//            }
//
//        } catch (Exception e) {
//            log.error("OAuth callback error", e);
//            return generateErrorPage(e.getMessage());
//        }
//    }

//    /**
//     * Export 처리 후 Notion URL 반환
//     */
//    private String processExportAndGetUrl(AiLog aiLog, NotionAccessToken token) {
//        try {
//            // Export 처리 로직 (기존 exportToNotion 메서드 내용 활용)
//            String workspaceInfo = findObjectTypeViaSearch(token.getAccessToken(), token.getUuid());
//
//            if (workspaceInfo != null) {
//                String[] parts = workspaceInfo.split(":");
//                if (parts.length == 2) {
//                    String objectType = parts[0];
//                    String targetObjectId = parts[1];
//
//                    if ("database".equals(objectType)) {
//                        notionExportService.exportToDatabasePage(aiLog, token.getAccessToken(), targetObjectId);
//                    } else if ("page".equals(objectType)) {
//                        notionExportService.exportToPageChild(aiLog, token.getAccessToken(), targetObjectId);
//                    }
//                }
//            } else {
//                // 기본 처리
//                String defaultPageId = notionExportService.findDefaultPageForExport(token.getAccessToken());
//                if (defaultPageId != null) {
//                    notionExportService.exportToPageChild(aiLog, token.getAccessToken(), defaultPageId);
//                } else {
//                    notionExportService.createStandalonePageFallback(aiLog, token.getAccessToken());
//                }
//            }
//
//            // 워크스페이스 URL 반환 (실제 페이지 URL은 export 결과에 따라 달라질 수 있음)
//            return "https://www.notion.so/";
//
//        } catch (Exception e) {
//            log.error("Export 처리 실패", e);
//            return null;
//        }
//    }

//    /**
//     * 성공 페이지 생성
//     */
//    private String generateSuccessPage(String notionPageUrl) {
//        String redirectScript = notionPageUrl != null ?
//            "<script>setTimeout(function() { window.location.href = '" + notionPageUrl + "'; }, 2000);</script>" :
//            "<script>window.close();</script>";
//
//        return "<html><body><h1>Notion 내보내기 완료</h1>"
//            + "<p>AI 분석 결과가 Notion에 성공적으로 저장되었습니다.</p>"
//            + (notionPageUrl != null ?
//            "<p><a href='" + notionPageUrl + "' target='_blank'>Notion에서 확인하기</a></p>" : "")
//            + redirectScript
//            + "</body></html>";
//    }
//
//    /**
//     * 에러 페이지 생성
//     */
//    private String generateErrorPage(String errorMessage) {
//        return "<html><body><h1>인증 실패</h1>"
//            + "<p>Notion 계정 연결 중 오류가 발생했습니다.</p>"
//            + "<p>오류: " + errorMessage + "</p>"
//            + "<p>다시 시도해 주세요. 이 창을 닫고 애플리케이션으로 돌아가세요.</p>"
//            + "<script>setTimeout(function() { window.close(); }, 5000);</script>"
//            + "</body></html>";
//    }