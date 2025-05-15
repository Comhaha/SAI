package com.sai.backend.domain.token.service;

import com.sai.backend.domain.token.service.TokenServiceImpl.TokenReloadedEvent;
import jakarta.annotation.PostConstruct;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.event.EventListener;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.scheduling.concurrent.ThreadPoolTaskScheduler;
import org.springframework.scheduling.support.PeriodicTrigger;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
@Slf4j
public class TokenRotationScheduler {

    @Autowired
    private final TokenService tokenService;
    private final ThreadPoolTaskScheduler taskScheduler;

    private ScheduledFuture<?> scheduledTask;
    private static final long ROTATION_PERIOD_MS = 10_800_000L;

    @PostConstruct
    public void init() {
        // 초기 스케줄링 설정
        scheduleTokenRotation();
    }

    private void scheduleTokenRotation() {
        if (scheduledTask != null && !scheduledTask.isCancelled()) {
            scheduledTask.cancel(false);
        }

        scheduledTask = taskScheduler.scheduleAtFixedRate(() -> {
            log.info("[Token] 3시간 주기 자동 재발급 시작");
            tokenService.scheduleReloadToken(); // 자동 재발급 메서드 호출
        }, ROTATION_PERIOD_MS);

        log.info("[Token] 스케줄링 재시작 - 다음 토큰 재발급: 3시간 후");
    }

    @EventListener
    public void handleTokenReloaded(TokenReloadedEvent event) {
        // 수동 재발급인 경우에만 스케줄 리셋
        if (event.isManual()) {
            log.info("[Token] 수동 토큰 재발급 이벤트 수신 - 스케줄 타이머 리셋");
            scheduleTokenRotation();
        } else {
            log.info("[Token] 자동 토큰 재발급 완료");
        }
    }
}
