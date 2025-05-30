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
        //log image 저장
        String logImageUrl = s3Service.uploadImage(dto.getLogImage());
        //result image 저장
        String resultImageUrl = s3Service.uploadImage(dto.getResultImage());

        String systemPrompt = """
    당신은 머신 러닝 모델 평가 전문가입니다.
    제공하는 python code, log image, 추론 결과 이미지, threshold 수치를 줄 겁니다.
    주의사항을 보고 답변에 주의하시기 바랍니다.
    
    ### 주의 사항
    1. 피드백에 로그, 결과 image url이 노출되서는 안 됩니다.
    2. 피드백 토큰은 최대 3000 토큰이니 잘 활용하여 작성해주시길 바랍니다.
    
    ## 분석 목표
    1. 제공된 코드와 학습 로그를 분석하여 모델의 학습 과정을 이해
    2. 로그 그래프에 대한 자세한 설명과 분석
    3. 결과 이미지를 분석하여 모델의 성능과 문제점 파악
    4. 구체적이고 실행 가능한 개선 방안 제시
    
    ## 분석 결과 형식
    분석 결과는 다음과 같은 구조로 제공해주세요:
    
    # 모델 분석 결과
    
    ## 현재 모델 현황
    - 모델 타입: [분석된 모델 타입]
    - 학습 방식: [학습 방법 설명]
    - 주요 특징: [코드에서 발견된 특징들]
    
    ## 성능 분석
    - 로그 그래프 설명: [로그 그래프에 대한 자세한 설명 + 수치에 변화에 따른 모델에 대한 평가]
    - 강점: [잘 수행된 부분]
    - 약점: [개선이 필요한 부분]
    - 결과 해석: [이미지 결과 분석]
    
    ## 개선 방안
    1. 즉시 적용 가능한 개선사항
    2. 중장기 개선 방안
    3. 추가 실험 제안
    
    """;

// 2) user 프롬프트를 StringBuilder 로 조립
        StringBuilder sb = new StringBuilder();
        sb.append("### 코드\n```python\n")
            .append(dto.getCode()).append("\n```\n\n")
            .append("### 로그 이미지\n")
            .append(logImageUrl).append("\n\n")
            .append("### 결과 이미지\n")
            .append("모델 추론 threshold 수치 : ").append(dto.getThreshold()).append("\n")
            .append(resultImageUrl).append("\n\n");

        String userPrompt = sb.toString();

// 3) 최종 body 에는 messages 안에 Map.of("content", systemPrompt/userPrompt) 만
        Map<String,Object> body = Map.of(
            "model",       model,
            "max_tokens",  3000,
            "temperature", 0.4,
            "messages", List.of(
                Map.of("role",    "system", "content", systemPrompt),
                Map.of("role",    "user",   "content", userPrompt)
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

        AiLog log = new AiLog(
            feedbackId, dto.getCode(),
            logImageUrl, dto.getThreshold(),
            resultImageUrl, dto.getMemo(), feedback);

        aiFeedbackRepository.save(log);

        String notionRedirectUrl = notionService.generateAuthUrl(feedbackId);

        return new AiFeedbackResponseDto(feedbackId, feedback, notionRedirectUrl);
    }

}
