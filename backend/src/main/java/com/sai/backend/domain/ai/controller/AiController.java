package com.sai.backend.domain.ai.controller;

import com.sai.backend.domain.ai.dto.request.AiFeedbackRequestDto;
import com.sai.backend.domain.ai.dto.response.AiFeedbackResponseDto;
import com.sai.backend.domain.ai.service.AiService;
import com.sai.backend.global.model.dto.BaseResponse;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestPart;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

@RestController
@RequiredArgsConstructor
public class AiController implements AiApi{

    private final AiService aiService;

    @Override
    public ResponseEntity<BaseResponse<AiFeedbackResponseDto>> feedback(
        String code, MultipartFile logImage,
        MultipartFile resultImage, String memo) {

        AiFeedbackRequestDto requestDto = new AiFeedbackRequestDto(code, logImage, resultImage, memo);
        return ResponseEntity.ok(new BaseResponse<>(aiService.feedback(requestDto)));
    }
}
