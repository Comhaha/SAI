package com.sai.backend.domain.notion.service;

import com.sai.backend.domain.ai.model.AiLog;
import com.sai.backend.domain.notion.dto.response.ExportResponseDto;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
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

    private static final int MAX_CHILDREN_PER_REQUEST = 100;

    private final WebClient notionWebClient;
    private final NotionContentParsingService contentParsingService;

    /**
     * 페이지의 하위 페이지로 추가 (children 100 초과 시 자동 append)
     */
    public ExportResponseDto exportToPageChild(AiLog aiLog, String accessToken, String pageId) {

        /* 1) 전체 children 생성 */
        List<Map<String, Object>> allChildren = contentParsingService.buildAiLogContent(aiLog);

        /* 2) 100 개씩 분할 */
        List<List<Map<String, Object>>> chunks = chunk(allChildren, MAX_CHILDREN_PER_REQUEST);

        /* 3-1) 첫 chunk 로 새 하위 페이지 생성 */
        Map<String, Object> firstChunkRequest = buildNotionPageContentForPage(aiLog, pageId);
        firstChunkRequest.put("children", chunks.get(0));

        Map<String, Object> createRes = postJson("/v1/pages", accessToken, firstChunkRequest);
        if (createRes == null) {
            return ExportResponseDto.builder()
                .success(false)
                .message("하위 페이지 생성 실패")
                .build();
        }

        String newPageId = (String) createRes.get("id");
        String pageUrl   = (String) createRes.get("url");
        log.info("하위 페이지 생성 성공: {}", pageUrl);

        /* 3-2) 남은 chunk 들을 append */
        for (int i = 1; i < chunks.size(); i++) {
            Map<String, Object> appendReq = Map.of("children", chunks.get(i));
            patchJson("/v1/blocks/" + newPageId + "/children", accessToken, appendReq);
        }

        return ExportResponseDto.builder()
            .success(true)
            .message("페이지 하위로 내보내기가 완료되었습니다.")
            .status(true)
            .url(pageUrl)
            .build();
    }

    /**
     * 데이터베이스에 페이지 추가 (children 100 초과 시 append)
     */
    public ExportResponseDto exportToDatabasePage(AiLog aiLog, String accessToken, String databaseId) {

        List<Map<String, Object>> allChildren = contentParsingService.buildAiLogContent(aiLog);
        List<List<Map<String, Object>>> chunks = chunk(allChildren, MAX_CHILDREN_PER_REQUEST);

        /* 1) 첫 chunk 로 row 생성 */
        Map<String, Object> firstPageReq = buildNotionPageContentForDatabase(aiLog, databaseId);
        firstPageReq.put("children", chunks.get(0));

        Map<String, Object> createRes = postJson("/v1/pages", accessToken, firstPageReq);
        if (createRes == null) {
            return ExportResponseDto.builder()
                .success(false)
                .message("데이터베이스 페이지 생성 실패")
                .build();
        }

        String newPageId = (String) createRes.get("id");
        String pageUrl   = (String) createRes.get("url");
        log.info("데이터베이스 페이지 생성 성공: {}", pageUrl);

        /* 2) 나머지 chunk append */
        for (int i = 1; i < chunks.size(); i++) {
            Map<String, Object> appendReq = Map.of("children", chunks.get(i));
            patchJson("/v1/blocks/" + newPageId + "/children", accessToken, appendReq);
        }

        return ExportResponseDto.builder()
            .success(true)
            .message("데이터베이스에 내보내기가 완료되었습니다.")
            .status(true)
            .url(pageUrl)
            .build();
    }

    /**
     * 워크스페이스 직접 생성 (children 100 초과 시 append)
     * ※ 기본 동작은 동일, chunk 로직 통일
     */
    public ExportResponseDto createStandalonePageFallback(AiLog aiLog, String accessToken) {

        List<Map<String, Object>> allChildren = contentParsingService.buildAiLogContent(aiLog);
        List<List<Map<String, Object>>> chunks = chunk(allChildren, MAX_CHILDREN_PER_REQUEST);

        DateTimeFormatter fmt = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
        String now = LocalDateTime.now(ZoneId.of("Asia/Seoul")).format(fmt);

        /* 1) 첫 chunk 로 stand-alone page 생성 */
        Map<String, Object> firstPageReq = new HashMap<>();
        firstPageReq.put("parent", Map.of("type", "workspace", "workspace", true));
        firstPageReq.put("properties", Map.of(
            "title", Map.of(
                "type", "title",
                "title", List.of(Map.of(
                    "type", "text",
                    "text", Map.of("content", "AI 분석 결과: " + now)
                ))
            )
        ));
        firstPageReq.put("children", chunks.get(0));

        Map<String, Object> createRes = postJson("/v1/pages", accessToken, firstPageReq);
        if (createRes == null) {
            return ExportResponseDto.builder()
                .success(false)
                .message("페이지 생성 실패")
                .build();
        }

        String newPageId = (String) createRes.get("id");
        String pageUrl   = (String) createRes.get("url");
        log.info("독립 페이지 생성 성공: {}", pageUrl);

        /* 2) 나머지 chunks append */
        for (int i = 1; i < chunks.size(); i++) {
            Map<String, Object> appendReq = Map.of("children", chunks.get(i));
            patchJson("/v1/blocks/" + newPageId + "/children", accessToken, appendReq);
        }

        return ExportResponseDto.builder()
            .success(true)
            .message("워크스페이스에 새 페이지로 내보내기가 완료되었습니다.")
            .status(true)
            .url(pageUrl)
            .build();
    }

    /*─────────────────────── PRIVATE UTIL ───────────────────────*/

    /** POST helper (error 로그/예외 처리 공통) */
    private Map<String, Object> postJson(String uri, String accessToken, Object body) {
        return notionWebClient
            .post()
            .uri(uri)
            .header("Authorization", "Bearer " + accessToken)
            .header("Notion-Version", "2022-06-28")
            .contentType(MediaType.APPLICATION_JSON)
            .bodyValue(body)
            .retrieve()
            .onStatus(httpStatus -> httpStatus.value() >= 400, clientResp ->
                clientResp.bodyToMono(String.class)
                    .doOnNext(err -> log.error("{} 실패 - Status: {}, Body: {}", uri, clientResp.statusCode(), err))
                    .then(Mono.error(new RuntimeException(uri + " 호출 실패"))))
            .bodyToMono(new ParameterizedTypeReference<Map<String, Object>>() {})
            .block();
    }

    /** list → N 크기씩 분할 */
    private <T> List<List<T>> chunk(List<T> src, int size) {
        List<List<T>> out = new ArrayList<>();
        for (int i = 0; i < src.size(); i += size) {
            out.add(src.subList(i, Math.min(i + size, src.size())));
        }
        return out;
    }

    private Map<String, Object> patchJson(String uri, String accessToken, Object body) {
        return notionWebClient
            .patch()                                                       // ★ PATCH
            .uri(uri)
            .header("Authorization", "Bearer " + accessToken)
            .header("Notion-Version", "2022-06-28")
            .contentType(MediaType.APPLICATION_JSON)
            .bodyValue(body)
            .retrieve()
            .onStatus(httpStatus -> httpStatus.value() >= 400, clientResp ->
                clientResp.bodyToMono(String.class)
                    .doOnNext(err -> log.error("{} 실패 - Status: {}, Body: {}", uri,
                        clientResp.statusCode(), err))
                    .then(Mono.error(new RuntimeException(uri + " 호출 실패"))))
            .bodyToMono(new ParameterizedTypeReference<Map<String,Object>>() {})
            .block();
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