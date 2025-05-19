package com.sai.backend.domain.notion.service;

import com.sai.backend.domain.ai.model.AiLog;
import java.util.HashMap;
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
                blocks.add(createHeadingBlock(line.substring(2).trim(), 2));
            } else if (line.startsWith("#")) {
                blocks.add(createHeadingBlock(line.substring(1).trim(), 1));
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

    /*────────────────────── Block 생성 헬퍼 메서드 ──────────────────────*/
    public Map<String,Object> createHeadingBlock(String text,int level){
        level=Math.max(1,Math.min(level,3));           // Notion은 1~3
        return Map.of(
            "object","block",
            "type","heading_"+level,
            "heading_"+level, Map.of(
                "rich_text",List.of(Map.of(
                    "type","text",
                    "text",Map.of("content",text==null?"":text)
                ))
            )
        );
    }
    public Map<String,Object> createParagraphBlock(String text){ return createParagraphFromMarkdown(text); }

    public Map<String,Object> createDividerBlock(){ return Map.of("object","block","type","divider","divider",Map.of()); }

    public Map<String,Object> createBulletedListItem(String text){
        return Map.of(
            "object","block","type","bulleted_list_item",
            "bulleted_list_item", Map.of("rich_text",List.of(Map.of(
                "type","text","text",Map.of("content",text)
            )))
        );
    }

    public Map<String,Object> createCodeBlock(String code,String lang){
        List<Map<String,Object>> richTextList = new ArrayList<>();
        int maxLength = 2000;
        for (int i = 0; i < code.length(); i += maxLength) {
            String chunk = code.substring(i, Math.min(i + maxLength, code.length()));
            richTextList.add(Map.of(
                "type", "text",
                "text", Map.of("content", chunk)
            ));
        }
        return Map.of(
            "object", "block",
            "type",   "code",
            "code",   Map.of(
                "rich_text", richTextList,
                "language",  lang
            )
        );
    }

    public Map<String,Object> createImageBlock(String url){
        return Map.of(
            "object","block","type","image",
            "image",Map.of("type","external","external",Map.of("url",url))
        );
    }

    /* 인라인 마크다운(**,*,_,`) 파싱 → Rich Text 리스트 */
    public Map<String,Object> createParagraphFromMarkdown(String text){
        List<Map<String,Object>> rich=new ArrayList<>();
        Pattern p=Pattern.compile("(`[^`\\n]+`|\\*\\*[^*\\n]+\\*\\*|__[^_\\n]+__|\\*[^*\\n]+\\*|_[^_\\n]+_|[^*_`]+)");
        Matcher m=p.matcher(text==null?"":text);
        while(m.find()){
            String part=m.group(1);
            if(part.startsWith("`")&&part.endsWith("`")){
                rich.add(richText(part.substring(1,part.length()-1),Map.of("code",true)));
            }else if((part.startsWith("**")&&part.endsWith("**"))||(part.startsWith("__")&&part.endsWith("__"))){
                rich.add(richText(part.substring(2,part.length()-2),Map.of("bold",true)));
            }else if((part.startsWith("*")&&part.endsWith("*"))||(part.startsWith("_")&&part.endsWith("_"))){
                rich.add(richText(part.substring(1,part.length()-1),Map.of("italic",true)));
            }else{
                rich.add(richText(part,null));
            }
        }
        return Map.of(
            "object","block","type","paragraph",
            "paragraph",Map.of("rich_text",rich)
        );
    }

    private Map<String,Object> richText(String content, Map<String,Boolean> annotations){
        Map<String,Object> node=Map.of(
            "type","text",
            "text",Map.of("content",content)
        );
        if(annotations!=null) node=new HashMap<>(node){{
            put("annotations",annotations);
        }};
        return node;
    }

    private boolean isNotBlank(String s){
        return s!=null && !s.trim().isEmpty();
    }
}