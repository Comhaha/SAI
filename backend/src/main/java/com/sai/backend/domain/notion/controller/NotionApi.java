package com.sai.backend.domain.notion.controller;

import com.sai.backend.domain.notion.dto.request.CallbackRequestDto;
import com.sai.backend.domain.notion.dto.request.ExportRequestDto;
import com.sai.backend.domain.notion.dto.response.AuthCheckResponseDto;
import com.sai.backend.domain.notion.dto.response.ExportResponseDto;
import com.sai.backend.global.model.dto.BaseResponse;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

@RequestMapping("/api/notion")
public interface NotionApi {

    @GetMapping("/auth/check")
    ResponseEntity<BaseResponse<AuthCheckResponseDto>> checkAuthStatus(@RequestParam String uuid);

    //Oauth 로그인 -> export
    @PostMapping("/export")
    ResponseEntity<BaseResponse<ExportResponseDto>> exportReport(@RequestBody ExportRequestDto requestDto);

    //Oauth 후 처리
    @GetMapping("/callback")
    ResponseEntity<BaseResponse<String>> handleCallback(@RequestParam String code, @RequestParam String state);
}
