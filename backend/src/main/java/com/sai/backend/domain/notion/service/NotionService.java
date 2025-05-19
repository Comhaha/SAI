package com.sai.backend.domain.notion.service;

import com.sai.backend.domain.notion.dto.request.CallbackRequestDto;
import com.sai.backend.domain.notion.dto.response.ExportResponseDto;
import org.springframework.stereotype.Service;

@Service
public interface NotionService {

    // OAuth URL 생성
    String generateAuthUrl(String uuid);

    // OAuth 콜백 처리
    ExportResponseDto handleOAuthCallback(CallbackRequestDto dto);
}
