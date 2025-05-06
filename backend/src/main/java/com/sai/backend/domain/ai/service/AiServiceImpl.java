package com.sai.backend.domain.ai.service;

import com.sai.backend.domain.ai.dto.request.AiFeedbackRequestDto;
import com.sai.backend.domain.ai.dto.response.AiFeedbackResponseDto;

import java.util.List;
import java.util.Map;

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
                            당신은 모델 평가 전문가야. code와 주석을 보고 학습과정에 대한 log를 분석,
                            image_url에 있는 추론에 대한 결과 분석하고 지금까지 작성된 코드는 어떤 방식으로
                            학습하고 더 고도화할 수 있는지 피드백 해줘.
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

        return new AiFeedbackResponseDto(feedback);
    }

}
