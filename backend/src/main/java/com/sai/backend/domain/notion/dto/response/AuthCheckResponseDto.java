package com.sai.backend.domain.notion.dto.response;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class AuthCheckResponseDto {
    private Boolean authenticated;  // 인증 여부
    private String authUrl;        // 인증이 필요한 경우 OAuth URL
}