package com.sai.backend.domain.notion.dto.response;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class ExportResponseDto {
    private Boolean success;        // 내보내기 성공 여부
    private String message;         // 성공/실패 메시지
    private Boolean status = false; // 기존 필드 유지 (하위 호환성)
    private String url;
}