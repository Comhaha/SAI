package com.sai.backend.domain.ai.service;

import com.sai.backend.domain.ai.dto.request.AiFeedbackRequestDto;
import com.sai.backend.domain.ai.dto.response.AiFeedbackResponseDto;

import com.sai.backend.domain.ai.model.AiLog;
import com.sai.backend.domain.ai.repository.mongo.AiFeedbackRepository;
import com.sai.backend.domain.notion.service.NotionService;
import java.util.List;
import java.util.Map;

import java.util.UUID;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClient;

@Service
@RequiredArgsConstructor
public class AiServiceImpl implements AiService {

    private final WebClient openAiWebClient;
    private final S3Service s3Service;
    private final NotionService notionService;
    private final AiFeedbackRepository aiFeedbackRepository;

    @Value("${openai.model}")
    private String model;

    @Override
    public AiFeedbackResponseDto feedback(AiFeedbackRequestDto dto) {
        //이미지 변환
        String dataUrl = s3Service.uploadImage(dto.getImage());

        Map<String,Object> body = Map.of(
            "model", model,
            "max_tokens", 600,
            "temperature", 0.4,
            "messages", List.of(
                Map.of("role", "system",
                    "content", """
                            당신은 머신 러닝 모델 평가 전문가야.
                            \s
                             ## 분석 목표
                             1. 제공된 코드와 학습 로그를 분석하여 모델의 학습 과정을 이해
                             2. 결과 이미지를 분석하여 모델의 성능과 문제점 파악
                             3. 구체적이고 실행 가능한 개선 방안 제시
                            \s
                             ## 분석 결과 형식
                             분석 결과는 다음과 같은 구조로 제공해주세요:
                            \s
                             # 모델 분석 결과
                            \s
                             ## 현재 모델 현황
                             - 모델 타입: [분석된 모델 타입]
                             - 학습 방식: [학습 방법 설명]
                             - 주요 특징: [코드에서 발견된 특징들]
                            \s
                             ## 성능 분석
                             - 강점: [잘 수행된 부분]
                             - 약점: [개선이 필요한 부분]
                             - 결과 해석: [이미지 결과 분석]
                            \s
                             ## 개선 방안
                             1. 즉시 적용 가능한 개선사항
                             2. 중장기 개선 방안
                             3. 추가 실험 제안
                        """),

                Map.of("role", "user",
                    "content", List.of(
                        Map.of("type", "text",
                            "text", "### 코드\n```python\n" + dto.getCode() + "\n```\n"
                                + "### 로그\n```\n" + dto.getLog() + "\n```"),
                        Map.of("type", "image_url",
                            "image_url", Map.of("url", dataUrl))
                    ))
            )
        );

        String feedback = openAiWebClient.post()
            .bodyValue(body)
            .retrieve()
            .bodyToMono(new ParameterizedTypeReference<Map<String,Object>>() {})
            .map(res -> (String) ((Map<?,?>)((Map<?,?>)((List<?>)res.get("choices")).get(0))
                .get("message")).get("content"))
            .block();

        String feedbackId = UUID.randomUUID().toString();

        AiLog log = new AiLog(feedbackId, dto.getCode(), dto.getLog(), dataUrl, feedback);
        aiFeedbackRepository.save(log);

        String notionRedirectUrl = notionService.generateAuthUrl(feedbackId);

        return new AiFeedbackResponseDto(feedbackId, feedback, notionRedirectUrl);
    }

}
