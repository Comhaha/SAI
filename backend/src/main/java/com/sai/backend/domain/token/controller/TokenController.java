package com.sai.backend.domain.token.controller;

import com.sai.backend.domain.token.dto.response.TokenResponseDto;
import com.sai.backend.domain.token.service.TokenService;
import com.sai.backend.global.model.dto.BaseResponse;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequiredArgsConstructor
@Slf4j
public class TokenController implements TokenApi{

    private final TokenService tokenService;

    @Override
    public ResponseEntity<BaseResponse<TokenResponseDto>> getToken() {
        return ResponseEntity.ok(new BaseResponse<>(tokenService.getRedisToken()));
    }

    @Override
    public ResponseEntity<BaseResponse<TokenResponseDto>> reloadToken() {
        return ResponseEntity.ok(new BaseResponse<>(tokenService.reloadToken()));
    }

    @Override
    public ResponseEntity<BaseResponse<Boolean>> checkToken(String authHeader) {
        return ResponseEntity.ok(new BaseResponse<>(tokenService.isValid(authHeader)));
    }

    @Override
    public ResponseEntity<BaseResponse<String>> getTokenTime() {
        return ResponseEntity.ok(new BaseResponse<>(tokenService.getTokenRemainingTime()));
    }

}
