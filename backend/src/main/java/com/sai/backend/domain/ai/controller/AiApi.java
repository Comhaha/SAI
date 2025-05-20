package com.sai.backend.domain.ai.controller;

import com.sai.backend.domain.ai.dto.request.AiFeedbackRequestDto;
import com.sai.backend.domain.ai.dto.response.AiFeedbackResponseDto;
import com.sai.backend.global.model.dto.BaseResponse;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestPart;
import org.springframework.web.multipart.MultipartFile;

@RequestMapping("/api/ai")
public interface AiApi {

    @PostMapping(value = "/feedback", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    ResponseEntity<BaseResponse<AiFeedbackResponseDto>> feedback(
        @RequestPart("code") String code,
        @RequestPart("logImage")  MultipartFile logImage,
        @RequestPart("resultImage") MultipartFile resultImage,
        @RequestPart("memo") String memo,
        @RequestPart("threshold") String threshold
    );
}
