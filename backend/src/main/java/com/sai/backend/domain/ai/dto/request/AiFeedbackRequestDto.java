package com.sai.backend.domain.ai.dto.request;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;
import org.springframework.web.multipart.MultipartFile;

@Getter
@Setter
@AllArgsConstructor
public class AiFeedbackRequestDto {
    private String code;
    private MultipartFile logImage;
    private MultipartFile resultImage;
    private String memo;
    private String threshold;
}
