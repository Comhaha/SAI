package com.sai.backend.domain.notion.service;

import com.sai.backend.domain.notion.dto.request.CallbackRequestDto;
import com.sai.backend.domain.notion.dto.response.ExportResponseDto;
import org.springframework.stereotype.Service;

@Service
public interface NotionService {

//     인증 상태 확인 및 OAuth URL 생성
//    AuthCheckResponseDto checkAuthStatus(String uuid);
//
//     Notion에 내보내기
//    ExportResponseDto exportToNotion(String uuid);

    // OAuth 콜백 처리
//    String handleOAuthCallback(CallbackRequestDto dto);

    // OAuth URL 생성
    String generateAuthUrl(String uuid);

    // OAuth 콜백 처리
    ExportResponseDto handleOAuthCallback(CallbackRequestDto dto);
}
