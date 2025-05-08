package com.sai.backend.domain.token.service;

import com.sai.backend.domain.token.dto.response.TokenResponseDto;
import com.sai.backend.domain.token.model.RedisToken;
import com.sai.backend.domain.token.model.Token;
import com.sai.backend.domain.token.repository.jpa.JpaTokenRepository;
import com.sai.backend.domain.token.repository.redis.RedisTokenRepository;
import com.sai.backend.global.util.TokenUtil;
import jakarta.annotation.PostConstruct;
import java.time.Duration;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.boot.context.event.ApplicationReadyEvent;
import org.springframework.context.event.EventListener;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@RequiredArgsConstructor
@Slf4j
public class TokenServiceImpl implements TokenService {

    private static final String KEY = "singleton-current-token";
    private static final Duration TTL = Duration.ofHours(3);

    private final TokenUtil tokenUtil;
    private final JpaTokenRepository jpaTokenRepository;
    private final RedisTokenRepository redisTokenRepository;

    @EventListener(ApplicationReadyEvent.class)
    public void onApplicationReady() {
        try {
            redisTokenRepository.findById(KEY).ifPresentOrElse(oldRedisToken -> {
                Token token = oldRedisToken.toEntity();
                jpaTokenRepository.save(token);      // 이전값 이력화
                replaceWithNewToken(oldRedisToken);
                log.info("[Token] 시작 시 Redis 값 존재 → 새 토큰 발급 완료");

            }, () -> {
                RedisToken newCt = createCurrentToken(tokenUtil.generate());
                redisTokenRepository.save(newCt);
                log.info("[Token] 시작 시 Redis 값 없음 → 새 토큰 발급 완료");
            });
        } catch (Exception e) {
            log.error("[Token] Redis 초기화 중 오류 발생", e);
        }
    }

    @Override
    public TokenResponseDto getRedisToken() {
        return redisTokenRepository.findById(KEY)
            .map(ct -> new TokenResponseDto(ct.getToken()))
            .orElseGet(() -> {                  // 이론상 거의 발생 X
                RedisToken newRedisToken = createCurrentToken(tokenUtil.generate());
                redisTokenRepository.save(newRedisToken);
                return new TokenResponseDto(newRedisToken.getToken());
            });
    }

    @Override
    @Transactional
    public TokenResponseDto reloadToken() {
        RedisToken redisToken = redisTokenRepository.findById(KEY)
            .orElseGet(() -> createCurrentToken(null));

        Token token = redisToken.toEntity();
        jpaTokenRepository.save(token);

        replaceWithNewToken(redisToken);

        return new TokenResponseDto(redisToken.getToken());
    }

    @Override
    public Boolean isValid(String token) {
        String checkToken = extractToken(token);
        return redisTokenRepository.findById(KEY)
            .map(rt -> checkToken != null && checkToken.equals(rt.getToken()))
            .orElse(false);
    }

    /* ───────────── 내부 공통 로직 ───────────── */
    private void replaceWithNewToken(RedisToken oldRedisToken) {
        oldRedisToken.setToken(tokenUtil.generate());
        oldRedisToken.setUseCount(0L);
        oldRedisToken.setExpiration(TTL.toSeconds());
        redisTokenRepository.save(oldRedisToken);
    }

    private RedisToken createCurrentToken(String value) {
        RedisToken rt = new RedisToken();
        rt.setToken(value != null ? value : tokenUtil.generate());
        rt.setExpiration(TTL.toSeconds());
        rt.setId(KEY);
        return rt;
    }

    private String extractToken(String header) {
        String PREFIX = "Bearer ";
        return (header != null && header.startsWith(PREFIX))
            ? header.substring(PREFIX.length())
            : null;
    }
}
