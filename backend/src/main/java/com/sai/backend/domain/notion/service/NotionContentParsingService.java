package com.sai.backend.domain.notion.service;

import com.sai.backend.domain.ai.model.AiLog;
import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

@Service
@Slf4j
public class NotionContentParsingService {

    /*────────────────────── public API ──────────────────────*/

    /** AI 로그를 Notion 블록 목록으로 변환 */
    public List<Map<String, Object>> buildAiLogContent(AiLog aiLog) {
        List<Map<String, Object>> children = new ArrayList<>();

        /* ① 코드 */
        if (isNotBlank(aiLog.getCode())) {
            children.add(createHeadingBlock("코드", 2));
            children.add(createCodeBlock(aiLog.getCode(), "python"));
        }

        /* ② 로그 이미지 */
        if (isNotBlank(aiLog.getLogImageUrl())) {
            children.add(createHeadingBlock("로그 이미지", 2));
            children.add(createImageBlock(aiLog.getLogImageUrl()));
        }

        /* ③ 결과 이미지 */
        if (isNotBlank(aiLog.getResultImageUrl())) {
            children.add(createHeadingBlock("결과 이미지", 2));

            if (aiLog.getThreshold() != null) {
                String th = "threshold : " + aiLog.getThreshold();
                children.add(createBulletedListItem(th));
            }

            children.add(createImageBlock(aiLog.getResultImageUrl()));
        }

        /* ④ 메모 */
        if (isNotBlank(aiLog.getMemo())) {
            children.add(createHeadingBlock("나의 메모", 2));
            children.add(createParagraphBlock(aiLog.getMemo()));
        }

        /* ⑤ 피드백 */
        if (isNotBlank(aiLog.getFeedback())) {
            children.add(createHeadingBlock("AI 피드백", 2));
            children.addAll(parseMarkdownToNotionBlocksLineByLine(aiLog.getFeedback()));
        }

        return children;
    }

    /** 마크다운 텍스트를 줄 단위로 Notion 블록으로 변환 */
    public List<Map<String, Object>> parseMarkdownToNotionBlocksLineByLine(String markdown) {
        List<Map<String, Object>> blocks = new ArrayList<>();
        String[] lines = markdown.split("\n");

        for (int i = 0; i < lines.length; i++) {
            String line = lines[i];

            /* 빈 줄 → 빈 문단 */
            if (line.trim().isEmpty()) {
                blocks.add(createParagraphBlock(""));
                continue;
            }

            /* 수평선(---) */
            if (line.trim().matches("^-{3,}$")) {
                blocks.add(createDividerBlock());
                continue;
            }

            /* 헤딩 */
            if (line.startsWith("###")) {
                blocks.add(createHeadingBlock(line.substring(3).trim(), 3));
                continue;
            } else if (line.startsWith("##")) {
                blocks.add(createHeadingBlock(line.substring(2).trim(), 2));
                continue;
            } else if (line.startsWith("#")) {
                blocks.add(createHeadingBlock(line.substring(1).trim(), 1));
                continue;
            }

            /* 코드 블록 */
            if (line.startsWith("```")) {
                String language = line.substring(3).trim();
                if (language.isEmpty()) language = "plain text";

                StringBuilder code = new StringBuilder();
                i++; // 다음 줄
                while (i < lines.length && !lines[i].startsWith("```")) {
                    if (code.length() > 0) code.append("\n");
                    code.append(lines[i]);
                    i++;
                }
                blocks.add(createCodeBlock(code.toString(), language));
                continue;
            }

            /* 불릿 리스트 */
            if (line.trim().startsWith("- ")) {
                blocks.add(createBulletedListItem(line.trim().substring(2)));
                continue;
            }

            /* 일반 문단 */
            blocks.add(createParagraphBlock(line));
        }

        return blocks;
    }

    /*────────────────────── Block 생성 메서드 ──────────────────────*/

    public Map<String,Object> createHeadingBlock(String text,int level){
        level = Math.max(1, Math.min(level, 3));  // Notion은 1~3
        return Map.of(
            "object","block",
            "type","heading_"+level,
            "heading_"+level, Map.of(
                "rich_text", parseInlineMarkdown(text)
            )
        );
    }

    public Map<String,Object> createParagraphBlock(String text){
        return Map.of(
            "object","block","type","paragraph",
            "paragraph", Map.of("rich_text", parseInlineMarkdown(text))
        );
    }

    public Map<String,Object> createDividerBlock(){
        return Map.of("object","block","type","divider","divider",Map.of());
    }

    public Map<String,Object> createBulletedListItem(String text){
        return Map.of(
            "object","block","type","bulleted_list_item",
            "bulleted_list_item", Map.of("rich_text", parseInlineMarkdown(text))
        );
    }

    public Map<String,Object> createCodeBlock(String code,String lang){
        List<Map<String,Object>> richText = new ArrayList<>();
        int MAX = 2000;
        for (int i = 0; i < code.length(); i += MAX) {
            richText.add(Map.of(
                "type","text",
                "text", Map.of("content", code.substring(i, Math.min(i+MAX, code.length())))
            ));
        }
        return Map.of(
            "object","block","type","code",
            "code", Map.of(
                "rich_text", richText,
                "language",  lang
            )
        );
    }

    public Map<String,Object> createImageBlock(String url){
        return Map.of(
            "object","block","type","image",
            "image", Map.of("type","external","external",Map.of("url",url))
        );
    }

    /*────────────────────── 인라인 마크다운 파서 ──────────────────────*/

    private List<Map<String,Object>> parseInlineMarkdown(String text){
        List<Map<String,Object>> rich = new ArrayList<>();
        Pattern p = Pattern.compile("(`[^`\\n]+`|\\*\\*[^*\\n]+\\*\\*|__[^_\\n]+__|\\*[^*\\n]+\\*|_[^_\\n]+_|[^*_`]+)");
        Matcher m = p.matcher(text == null ? "" : text);
        while (m.find()) {
            String part = m.group(1);
            Map<String,Object> node;
            if (part.startsWith("`") && part.endsWith("`")) {            // code
                node = richTextNode(part.substring(1, part.length()-1), Map.of("code", true));
            } else if ((part.startsWith("**") && part.endsWith("**")) || (part.startsWith("__") && part.endsWith("__"))) { // bold
                node = richTextNode(part.substring(2, part.length()-2), Map.of("bold", true));
            } else if ((part.startsWith("*") && part.endsWith("*")) || (part.startsWith("_") && part.endsWith("_"))) {     // italic
                node = richTextNode(part.substring(1, part.length()-1), Map.of("italic", true));
            } else {                                                      // plain
                node = richTextNode(part, null);
            }
            rich.add(node);
        }
        return rich;
    }

    private Map<String,Object> richTextNode(String content, Map<String,Boolean> ann){
        Map<String,Object> node = new HashMap<>();
        node.put("type","text");
        node.put("text", Map.of("content", content));
        if (ann != null) node.put("annotations", ann);
        return node;
    }

    /*────────────────────── Util ──────────────────────*/

    private boolean isNotBlank(String s){ return s != null && !s.trim().isEmpty(); }
}
