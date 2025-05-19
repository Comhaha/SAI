package com.sai.backend.domain.notion.service;

import com.sai.backend.domain.ai.model.AiLog;
import com.sai.backend.domain.notion.dto.response.ExportResponseDto;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClient;
import reactor.core.publisher.Mono;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Service
@RequiredArgsConstructor
@Slf4j
public class NotionExportService {

    private final WebClient notionWebClient;
    private final NotionContentParsingService contentParsingService;

    /**
     * 데이터베이스에 페이지를 추가하는 메서드
     */
    public ExportResponseDto exportToDatabasePage(AiLog aiLog, String accessToken, String databaseId) {
        try {
            log.info("데이터베이스에 페이지 추가: {}", databaseId);
            Map<String, Object> pageContent = buildNotionPageContentForDatabase(aiLog, databaseId);

            Map<String, Object> response = notionWebClient
                .post()
                .uri("/v1/pages")
                .header("Authorization", "Bearer " + accessToken)
                .header("Notion-Version", "2022-06-28")
                .contentType(MediaType.APPLICATION_JSON)
                .bodyValue(pageContent)
                .retrieve()
                .onStatus(httpStatus -> httpStatus.value() >= 400, clientResponse -> {
                    return clientResponse.bodyToMono(String.class)
                        .doOnNext(errorBody -> log.error("데이터베이스 페이지 생성 실패 - Status: {}, Body: {}",
                            clientResponse.statusCode(), errorBody))
                        .then(Mono.error(new RuntimeException("데이터베이스 페이지 생성 실패: " + clientResponse.statusCode())));
                })
                .bodyToMono(new ParameterizedTypeReference<Map<String, Object>>() {})
                .block();

            if (response != null) {
                String pageUrl = (String) response.get("url");
                log.info("데이터베이스에 페이지 성공적으로 생성됨. URL: {}", pageUrl);

                return ExportResponseDto.builder()
                    .success(true)
                    .message("기존 데이터베이스에 페이지가 추가되었습니다.")
                    .status(true)
                    .url(pageUrl)
                    .build();
            }
        } catch (Exception e) {
            log.error("데이터베이스 페이지 추가 실패", e);
        }

        return ExportResponseDto.builder()
            .success(false)
            .message("데이터베이스 페이지 추가에 실패했습니다.")
            .build();
    }

    /**
     * 페이지의 하위 페이지로 추가하는 메서드
     */
    public ExportResponseDto exportToPageChild(AiLog aiLog, String accessToken, String pageId) {
        try {
            log.info("페이지의 하위 페이지로 추가: {}", pageId);
            Map<String, Object> pageContent = buildNotionPageContentForPage(aiLog, pageId);

            Map<String, Object> response = notionWebClient
                .post()
                .uri("/v1/pages")
                .header("Authorization", "Bearer " + accessToken)
                .header("Notion-Version", "2022-06-28")
                .contentType(MediaType.APPLICATION_JSON)
                .bodyValue(pageContent)
                .retrieve()
                .onStatus(httpStatus -> httpStatus.value() >= 400, clientResponse -> {
                    return clientResponse.bodyToMono(String.class)
                        .doOnNext(errorBody -> log.error("하위 페이지 생성 실패 - Status: {}, Body: {}",
                            clientResponse.statusCode(), errorBody))
                        .then(Mono.error(new RuntimeException("하위 페이지 생성 실패: " + clientResponse.statusCode())));
                })
                .bodyToMono(new ParameterizedTypeReference<Map<String, Object>>() {})
                .block();

            if (response != null) {
                String pageUrl = (String) response.get("url");
                log.info("페이지의 하위 페이지 성공적으로 생성됨. URL: {}", pageUrl);

                return ExportResponseDto.builder()
                    .success(true)
                    .message("기존 페이지의 하위 페이지로 추가되었습니다.")
                    .status(true)
                    .url(pageUrl)
                    .build();
            }
        } catch (Exception e) {
            log.error("하위 페이지 추가 실패", e);
        }

        return ExportResponseDto.builder()
            .success(false)
            .message("하위 페이지 추가에 실패했습니다.")
            .build();
    }

    /**
     * 독립적인 페이지 생성 (워크스페이스에 직접 생성)
     */
    public ExportResponseDto createStandalonePageFallback(AiLog aiLog, String accessToken) {
        try {
            log.info("워크스페이스에 독립 페이지 생성 시도");

            Map<String, Object> pageRequest = new HashMap<>();

            // 워크스페이스에 직접 생성 (parent 설정 없음)
            pageRequest.put("parent", Map.of("type", "workspace", "workspace", true));

            // 페이지 속성 설정
            Map<String, Object> properties = new HashMap<>();
            properties.put("title", Map.of(
                "type", "title",
                "title", List.of(Map.of(
                    "type", "text",
                    "text", Map.of("content", "AI 분석 결과: " + aiLog.getId())
                ))
            ));
            pageRequest.put("properties", properties);

            // 페이지 내용 추가
            List<Map<String, Object>> children = contentParsingService.buildAiLogContent(aiLog);
            pageRequest.put("children", children);

            Map<String, Object> response = notionWebClient
                .post()
                .uri("/v1/pages")
                .header("Authorization", "Bearer " + accessToken)
                .header("Notion-Version", "2022-06-28")
                .contentType(MediaType.APPLICATION_JSON)
                .bodyValue(pageRequest)
                .retrieve()
                .bodyToMono(new ParameterizedTypeReference<Map<String, Object>>() {})
                .block();

            if (response != null) {
                String pageUrl = (String) response.get("url");
                log.info("워크스페이스에 페이지 성공적으로 생성됨. URL: {}", pageUrl);

                return ExportResponseDto.builder()
                    .success(true)
                    .message("워크스페이스에 새 페이지로 내보내기가 완료되었습니다.")
                    .status(true)
                    .url(pageUrl)
                    .build();
            }
        } catch (Exception e) {
            log.error("독립 페이지 생성 실패", e);
        }

        return ExportResponseDto.builder()
            .success(false)
            .message("페이지 생성에 실패했습니다. Notion 권한을 확인해주세요.")
            .build();
    }

    /**
     * 기본 내보내기 페이지 찾기
     */
    public String findDefaultPageForExport(String accessToken) {
        try {
            log.info("기본 내보내기 페이지 찾기 시작");

            // 검색 요청 생성
            Map<String, Object> searchRequest = new HashMap<>();
            searchRequest.put("query", "");
            searchRequest.put("page_size", 10);

            Map<String, Object> searchResponse = notionWebClient
                .post()
                .uri("/v1/search")
                .header("Authorization", "Bearer " + accessToken)
                .header("Notion-Version", "2022-06-28")
                .contentType(MediaType.APPLICATION_JSON)
                .bodyValue(searchRequest)
                .retrieve()
                .bodyToMono(new ParameterizedTypeReference<Map<String, Object>>() {})
                .block();

            if (searchResponse != null && searchResponse.containsKey("results")) {
                List<Map<String, Object>> results = (List<Map<String, Object>>) searchResponse.get("results");

                // 첫 번째 페이지 반환
                for (Map<String, Object> result : results) {
                    String object = (String) result.get("object");
                    if ("page".equals(object)) {
                        String pageId = (String) result.get("id");
                        log.info("기본 페이지 ID 발견: {}", pageId);
                        return pageId;
                    }
                }
            }
        } catch (Exception e) {
            log.error("기본 페이지 찾기 실패", e);
        }

        return null;
    }

    /**
     * 페이지용 페이지 컨텐츠 생성
     */
    private Map<String, Object> buildNotionPageContentForPage(AiLog aiLog, String pageId) {
        Map<String, Object> content = new HashMap<>();

        // 페이지를 부모로 설정
        content.put("parent", Map.of("page_id", pageId));

        // properties 구조
        Map<String, Object> properties = new HashMap<>();
        properties.put("title", Map.of(
            "type", "title",
            "title", List.of(Map.of(
                "type", "text",
                "text", Map.of("content", "AI 분석 결과: " + aiLog.getId())
            ))
        ));

        content.put("properties", properties);

        // 페이지 블록 내용
        content.put("children", contentParsingService.buildAiLogContent(aiLog));

        return content;
    }

    /**
     * 데이터베이스용 페이지 컨텐츠 생성
     */
    private Map<String, Object> buildNotionPageContentForDatabase(AiLog aiLog, String databaseId) {
        Map<String, Object> content = new HashMap<>();

        // 데이터베이스를 부모로 설정
        content.put("parent", Map.of("database_id", databaseId));

        // 데이터베이스 속성 설정 (기존 속성이 있다면 사용, 없으면 기본값)
        Map<String, Object> properties = new HashMap<>();

        // 제목 속성은 거의 모든 데이터베이스에 있음
        properties.put("제목", Map.of(
            "title", List.of(Map.of(
                "type", "text",
                "text", Map.of("content", "AI 분석 결과: " + aiLog.getId())
            ))
        ));

        // 데이터베이스의 다른 속성들은 체크하지 않고 그냥 생성
        // Notion이 자동으로 처리하도록 함

        content.put("properties", properties);

        // 페이지 내용
        content.put("children", contentParsingService.buildAiLogContent(aiLog));

        return content;
    }
}