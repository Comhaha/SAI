package com.sai.backend.domain.ai.service;

import com.sai.backend.domain.ai.dto.request.AiFeedbackRequestDto;
import com.sai.backend.domain.ai.dto.response.AiFeedbackResponseDto;
import org.springframework.stereotype.Service;

@Service
public interface AiService {

    AiFeedbackResponseDto feedback(AiFeedbackRequestDto aiFeedbackRequestDto);
}
