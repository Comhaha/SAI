package com.sai.backend.domain.ai.dto.response;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
public class AiFeedbackResponseDto {
    private String feedbackId;
    private String feedback;
    private String redirectUrl;
}
