package com.sai.backend.domain.token.controller;

import com.sai.backend.domain.token.dto.response.TokenResponseDto;
import com.sai.backend.global.model.dto.BaseResponse;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/api/token")
public interface TokenApi {

    @GetMapping
    ResponseEntity<BaseResponse<TokenResponseDto>> getToken();

    @PostMapping("/reload")
    ResponseEntity<BaseResponse<TokenResponseDto>> reloadToken();

    @PostMapping("/check")
    ResponseEntity<BaseResponse<Boolean>> checkToken(
        @RequestHeader("Authorization") String authHeader);
}
