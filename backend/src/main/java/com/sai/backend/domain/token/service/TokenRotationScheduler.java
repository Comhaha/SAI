package com.sai.backend.domain.token.service;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
@Slf4j
public class TokenRotationScheduler {
    private final TokenService tokenService;

    @Scheduled(fixedRate = 10_800_000) // 3시간 마다
    public void rotate() {
        log.info("[Token] 3시간 주기 자동 재발급 시작");
        tokenService.reloadToken();
    }
}
