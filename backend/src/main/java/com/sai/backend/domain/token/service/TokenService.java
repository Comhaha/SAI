package com.sai.backend.domain.token.service;

import com.sai.backend.domain.token.dto.response.TokenResponseDto;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

@Service
public interface TokenService {
    TokenResponseDto getRedisToken();
    TokenResponseDto reloadToken();
    Boolean isValid(String token);
    String getTokenRemainingTime();
    void scheduleReloadToken();
}
