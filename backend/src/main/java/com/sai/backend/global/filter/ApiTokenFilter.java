package com.sai.backend.global.filter;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sai.backend.domain.token.model.RedisToken;
import com.sai.backend.domain.token.repository.redis.RedisTokenRepository;
import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.List;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.http.MediaType;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.web.util.matcher.AntPathRequestMatcher;
import org.springframework.web.filter.OncePerRequestFilter;

/** /api/ai/** 전용 토큰 필터  */
@Slf4j
@RequiredArgsConstructor
public class ApiTokenFilter extends OncePerRequestFilter {

    private static final String HEADER    = "Authorization";
    private static final String PREFIX    = "Bearer ";
    private static final String REDIS_KEY = "singleton-current-token";
    private static final String CNT_KEY   = "singleton-current-token:cnt";

    private final RedisTokenRepository redisRepo;
    private final RedisTemplate<String, Object> redisTemplate;
    private final ObjectMapper mapper = new ObjectMapper();
    private final AntPathRequestMatcher aiMatcher =
        new AntPathRequestMatcher("/api/ai/**");

    /** /api/ai/** 가 아니면 필터를 건너뜀 */
    @Override
    protected boolean shouldNotFilter(HttpServletRequest req) {
        return !aiMatcher.matches(req);
    }

    @Override
    protected void doFilterInternal(HttpServletRequest req,
        HttpServletResponse res,
        FilterChain chain)
        throws ServletException, IOException {

        /* 1) 헤더 파싱 */
        String raw = req.getHeader(HEADER);
        if (raw == null || !raw.startsWith(PREFIX)) {
            reject(res, "토큰이 존재하지 않습니다.");
            return;
        }
        String token = raw.substring(PREFIX.length());

        /* 2) 토큰 비교 */
        RedisToken ct = redisRepo.findById(REDIS_KEY).orElse(null);
        if (ct == null || !token.equals(ct.getToken())) {
            reject(res, "유효하지 않은 토큰입니다.");
            return;
        }

        /* 3) 원자적 사용 횟수 +1 */
        redisTemplate.opsForValue().increment(CNT_KEY);

        /* 4) SecurityContext 주입 */
        UsernamePasswordAuthenticationToken auth =
            new UsernamePasswordAuthenticationToken(
                "ai-client", token,
                List.of(new SimpleGrantedAuthority("AI")));
        SecurityContextHolder.getContext().setAuthentication(auth);

        chain.doFilter(req, res);
    }

    /* 401 JSON 응답 */
    private void reject(HttpServletResponse res, String msg) throws IOException {
        res.setStatus(HttpServletResponse.SC_UNAUTHORIZED);
        res.setContentType(MediaType.APPLICATION_JSON_VALUE);
        res.getWriter().write(
            mapper.writeValueAsString(
                new com.sai.backend.global.model.dto.BaseResponse<>(
                    com.sai.backend.global.model.dto.BaseResponseStatus.UNAUTHORIZED)));
    }
}
