package com.sai.backend.domain.token.controller;

import com.sai.backend.domain.token.dto.response.TokenResponseDto;
import com.sai.backend.domain.token.service.TokenService;
import com.sai.backend.global.model.dto.BaseResponse;
import com.sai.backend.global.model.dto.BaseResponseStatus;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequiredArgsConstructor
@Slf4j
public class TokenController implements TokenApi{

    private final TokenService tokenService;

    @Override
    public ResponseEntity<BaseResponse<TokenResponseDto>> getToken() {
        if (isAdminAuthenticated()) {
            return ResponseEntity.status(401)
                .body(new BaseResponse<>(BaseResponseStatus.UNAUTHORIZED));
        }
        return ResponseEntity.ok(new BaseResponse<>(tokenService.getRedisToken()));
    }

    @Override
    public ResponseEntity<BaseResponse<TokenResponseDto>> reloadToken() {
        if (isAdminAuthenticated()) {
            return ResponseEntity.status(401)
                .body(new BaseResponse<>(BaseResponseStatus.UNAUTHORIZED));
        }
        return ResponseEntity.ok(new BaseResponse<>(tokenService.reloadToken()));
    }

    @Override
    public ResponseEntity<BaseResponse<Boolean>> checkToken(String authHeader) {
        return ResponseEntity.ok(new BaseResponse<>(tokenService.isValid(authHeader)));
    }

    @Override
    public ResponseEntity<BaseResponse<String>> getTokenTime() {
        if (isAdminAuthenticated()) {
            return ResponseEntity.status(401)
                .body(new BaseResponse<>(BaseResponseStatus.UNAUTHORIZED));
        }
        return ResponseEntity.ok(new BaseResponse<>(tokenService.getTokenRemainingTime()));
    }

    /**
     * 현재 요청자가 ADMIN 권한을 가지고 있는지 확인
     */
    private boolean isAdminAuthenticated() {
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();

        if (auth == null || !auth.isAuthenticated()) {
            log.warn("No authentication found for token API access");
            return true;
        }

        boolean hasAdminAuthority = auth.getAuthorities().stream()
            .anyMatch(authority -> "ADMIN".equals(authority.getAuthority()));

        if (!hasAdminAuthority) {
            log.warn("Non-admin user attempted to access token API: {}", auth.getName());
            return true;
        }

        log.debug("Admin authentication verified for user: {}", auth.getName());
        return false;
    }

}
