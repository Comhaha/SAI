package com.sai.backend.domain.notion.service;

import com.sai.backend.domain.ai.model.AiLog;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

@Service
@Slf4j
public class NotionContentParsingService {

    /**
     * AI 로그 내용을 Notion 블록으로 변환
     */
    public List<Map<String, Object>> buildAiLogContent(AiLog aiLog) {
        List<Map<String, Object>> children = new ArrayList<>();

        // 코드 블록 추가
        if (aiLog.getCode() != null && !aiLog.getCode().trim().isEmpty()) {
            children.add(createHeadingBlock("코드", 2));
            children.add(createCodeBlock(aiLog.getCode(), "python"));
        }

        // 로그 블록 추가
        if (aiLog.getLog() != null && !aiLog.getLog().trim().isEmpty()) {
            children.add(createHeadingBlock("로그", 2));
            children.add(createCodeBlock(aiLog.getLog(), "plain text"));
        }

        // 피드백 블록 추가
        if (aiLog.getFeedback() != null && !aiLog.getFeedback().trim().isEmpty()) {
            children.add(createHeadingBlock("분석 결과", 2));
            List<Map<String, Object>> parsedBlocks = parseMarkdownToNotionBlocksLineByLine(aiLog.getFeedback());
            children.addAll(parsedBlocks);
        }

        // 이미지 블록 추가
        if (aiLog.getImageUrl() != null && !aiLog.getImageUrl().trim().isEmpty()) {
            children.add(createHeadingBlock("결과 이미지", 2));
            children.add(createImageBlock(aiLog.getImageUrl()));
        }

        return children;
    }

    /**
     * 마크다운 텍스트를 줄 단위로 Notion 블록으로 변환
     */
    public List<Map<String, Object>> parseMarkdownToNotionBlocksLineByLine(String markdown) {
        List<Map<String, Object>> blocks = new ArrayList<>();

        String[] lines = markdown.split("\n");

        for (int i = 0; i < lines.length; i++) {
            String line = lines[i];

            // 빈 줄은 빈 문단 블록으로 변환
            if (line.trim().isEmpty()) {
                blocks.add(createParagraphBlock(""));
                continue;
            }

            // 가로선 처리 (--- 또는 여러 개의 -)
            if (line.trim().matches("^-{3,}$")) {
                blocks.add(createDividerBlock());
                continue;
            }

            // 헤딩 처리 - 각 줄을 개별적으로 처리
            if (line.startsWith("###")) {
                blocks.add(createHeadingBlock(line.substring(3).trim(), 3));
            } else if (line.startsWith("##")) {
                blocks.add(createHeadingBlock(line.substring(2).trim(), 3));
            } else if (line.startsWith("#")) {
                blocks.add(createHeadingBlock(line.substring(1).trim(), 2));
            }
            // 코드 블록 시작 처리
            else if (line.startsWith("```")) {
                // 코드 언어 추출
                String language = line.substring(3).trim();
                if (language.isEmpty()) {
                    language = "plain text";
                }

                // 코드 블록 내용 수집
                StringBuilder codeContent = new StringBuilder();
                i++; // 다음 줄로 이동
                while (i < lines.length && !lines[i].startsWith("```")) {
                    if (codeContent.length() > 0) {
                        codeContent.append("\n");
                    }
                    codeContent.append(lines[i]);
                    i++;
                }

                blocks.add(createCodeBlock(codeContent.toString(), language));
            }
            // 불릿 포인트 처리 - 각 줄을 개별적으로 처리
            else if (line.trim().startsWith("- ")) {
                blocks.add(createBulletedListItem(line.trim().substring(2).trim()));
            }
            // 그 외 모든 줄은 마크다운 문단으로 처리
            else {
                blocks.add(createParagraphFromMarkdown(line));
            }
        }

        return blocks;
    }

    /**
     * 마크다운의 굵은 글씨(** 또는 __), 이탤릭체(* 또는 _), 인라인 코드(`) 처리를 포함한 문단 생성
     */
    public Map<String, Object> createParagraphFromMarkdown(String text) {
        List<Map<String, Object>> richTextList = parseInlineMarkdown(text);

        return Map.of(
            "object", "block",
            "type", "paragraph",
            "paragraph", Map.of(
                "rich_text", richTextList
            )
        );
    }

    /**
     * 인라인 마크다운 요소 파싱 (굵게, 이탤릭체, 인라인 코드)
     */
    private List<Map<String, Object>> parseInlineMarkdown(String text) {
        List<Map<String, Object>> richTextList = new ArrayList<>();

        // 연속된 ** 문자 패턴 처리 (마크다운이 아닌 실제 문자)
        if (text.contains("**") && !text.matches(".*\\*\\*[^*\\n]+\\*\\*.*")) {
            // ** 문자 자체를 표시하는 경우
            String[] parts = text.split("\\*\\*");
            for (int i = 0; i < parts.length; i++) {
                if (!parts[i].isEmpty()) {
                    richTextList.add(Map.of(
                        "type", "text",
                        "text", Map.of("content", parts[i])
                    ));
                }
                if (i < parts.length - 1) {
                    richTextList.add(Map.of(
                        "type", "text",
                        "text", Map.of("content", "**"),
                        "annotations", Map.of("bold", true)
                    ));
                }
            }
            return richTextList;
        }

        // 정규식으로 마크다운 패턴 찾기
        Pattern pattern = Pattern.compile("(```[\\s\\S]*?```|`[^`\n]*`|\\*\\*[^*\n]+\\*\\*|__[^_\n]+__|\\*[^*\n]+\\*|_[^_\n]+_|[^*_`]+)");
        Matcher matcher = pattern.matcher(text);

        while (matcher.find()) {
            String match = matcher.group(1);

            if (match.startsWith("`") && match.endsWith("`") && !match.startsWith("```")) {
                // 인라인 코드
                String code = match.substring(1, match.length() - 1);
                richTextList.add(Map.of(
                    "type", "text",
                    "text", Map.of("content", code),
                    "annotations", Map.of("code", true)
                ));
            } else if ((match.startsWith("**") && match.endsWith("**")) ||
                (match.startsWith("__") && match.endsWith("__"))) {
                // 굵은 글씨
                String bold = match.substring(2, match.length() - 2);
                richTextList.add(Map.of(
                    "type", "text",
                    "text", Map.of("content", bold),
                    "annotations", Map.of("bold", true)
                ));
            } else if ((match.startsWith("*") && match.endsWith("*")) ||
                (match.startsWith("_") && match.endsWith("_"))) {
                // 이탤릭체
                String italic = match.substring(1, match.length() - 1);
                richTextList.add(Map.of(
                    "type", "text",
                    "text", Map.of("content", italic),
                    "annotations", Map.of("italic", true)
                ));
            } else {
                // 일반 텍스트
                if (!match.trim().isEmpty()) {
                    richTextList.add(Map.of(
                        "type", "text",
                        "text", Map.of("content", match)
                    ));
                }
            }
        }

        return richTextList;
    }

    /**
     * 가로선(Divider) 블록 생성
     */
    public Map<String, Object> createDividerBlock() {
        return Map.of(
            "object", "block",
            "type", "divider",
            "divider", Map.of()  // 빈 객체
        );
    }

    /**
     * 불릿 리스트 아이템 생성
     */
    public Map<String, Object> createBulletedListItem(String text) {
        return Map.of(
            "object", "block",
            "type", "bulleted_list_item",
            "bulleted_list_item", Map.of(
                "rich_text", List.of(Map.of(
                    "type", "text",
                    "text", Map.of("content", text)
                ))
            )
        );
    }

    /**
     * 헤딩 블록 생성
     */
    public Map<String, Object> createHeadingBlock(String text, int level) {
        if (text == null || text.trim().isEmpty()) {
            text = "제목 없음";
        }

        // Notion은 최대 3레벨까지만 지원
        if (level > 3) {
            level = 3;
        } else if (level < 1) {
            level = 1;
        }

        String headingType = "heading_" + level;
        return Map.of(
            "object", "block",
            "type", headingType,
            headingType, Map.of(
                "rich_text", List.of(Map.of(
                    "type", "text",
                    "text", Map.of("content", text)
                ))
            )
        );
    }

    /**
     * 문단 블록 생성
     */
    public Map<String, Object> createParagraphBlock(String text) {
        if (text == null || text.trim().isEmpty()) {
            text = "";
        }

        // 긴 텍스트 처리를 위한 청크 분할
        List<Map<String, Object>> richTextList = new ArrayList<>();
        int maxLength = 2000; // Notion의 제한

        for (int i = 0; i < text.length(); i += maxLength) {
            String chunk = text.substring(i, Math.min(i + maxLength, text.length()));
            richTextList.add(Map.of(
                "type", "text",
                "text", Map.of("content", chunk)
            ));
        }

        return Map.of(
            "object", "block",
            "type", "paragraph",
            "paragraph", Map.of(
                "rich_text", richTextList
            )
        );
    }

    /**
     * 코드 블록 생성
     */
    public Map<String, Object> createCodeBlock(String code, String language) {
        if (code == null || code.trim().isEmpty()) {
            code = "코드 없음";
        }

        // 긴 코드 처리를 위한 청크 분할
        List<Map<String, Object>> richTextList = new ArrayList<>();
        int maxLength = 2000; // Notion의 제한

        for (int i = 0; i < code.length(); i += maxLength) {
            String chunk = code.substring(i, Math.min(i + maxLength, code.length()));
            richTextList.add(Map.of(
                "type", "text",
                "text", Map.of("content", chunk)
            ));
        }

        return Map.of(
            "object", "block",
            "type", "code",
            "code", Map.of(
                "rich_text", richTextList,
                "language", language
            )
        );
    }

    /**
     * 이미지 블록 생성
     */
    public Map<String, Object> createImageBlock(String imageUrl) {
        return Map.of(
            "object", "block",
            "type", "image",
            "image", Map.of(
                "type", "external",
                "external", Map.of(
                    "url", imageUrl
                )
            )
        );
    }
}