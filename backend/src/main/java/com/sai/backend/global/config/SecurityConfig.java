package com.sai.backend.global.config;

import com.sai.backend.domain.token.repository.redis.RedisTokenRepository;
import com.sai.backend.global.filter.ApiTokenFilter;
import java.util.Arrays;
import lombok.RequiredArgsConstructor;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.annotation.Order;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.security.config.Customizer;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.session.InvalidSessionStrategy;
import org.springframework.web.cors.CorsConfiguration;
import org.springframework.web.cors.CorsConfigurationSource;
import org.springframework.web.cors.UrlBasedCorsConfigurationSource;

@Configuration
@EnableWebSecurity
@RequiredArgsConstructor
public class SecurityConfig {

    private final RedisTokenRepository redisTokenRepository;
    private final RedisTemplate<String, Object> redisTemplate;

    @Bean
    public BCryptPasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }

    /*─────────────────  admin 전용 체인 (stateful) ─────────────────*/
    @Bean
    @Order(1)
    public SecurityFilterChain adminChain(HttpSecurity http) throws Exception {

        http.securityMatcher("/api/admin/**")                  // admin 하위 URL만
            .csrf(AbstractHttpConfigurer::disable)
            .sessionManagement(s -> s
                .sessionCreationPolicy(SessionCreationPolicy.IF_REQUIRED)
                .invalidSessionStrategy(refererRedirect())   // 세션 만료 시 이전 페이지로
            )
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/api/admin/login", "/api/admin/register", "/api/admin/change").permitAll()
                .anyRequest().authenticated()
            )
            .formLogin(form -> form                         // HTML form 대신 JSON-API 사용 시 커스터마이즈
                .loginPage("/admin/login")             // 401 → 이 엔드포인트 호출
                .successHandler((req, res, auth) -> {})// 실제 로직은 Controller에서 처리
                .permitAll()
            )
            .logout(Customizer.withDefaults());

        return http.build();
    }

    /*───────────────── ②  나머지 API (stateless) ─────────────────*/
    @Bean
    @Order(2)
    public SecurityFilterChain apiChain(HttpSecurity http) throws Exception {

        ApiTokenFilter apiTokenFilter =
            new ApiTokenFilter(redisTokenRepository, redisTemplate);

        http.csrf(AbstractHttpConfigurer::disable)
            .sessionManagement(s -> s.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/api/download/**").permitAll()
                .requestMatchers("/api/ai/**").authenticated()
                .anyRequest().permitAll())
            .addFilterBefore(apiTokenFilter,
                org.springframework.security.web.authentication.
                    UsernamePasswordAuthenticationFilter.class);


        return http.build();
    }

    /*───────────────── 커스텀 세션 만료 전략 ─────────────────*/
    private InvalidSessionStrategy refererRedirect() {
        return (request, response) -> {
            String referer = request.getHeader("Referer");
            response.sendRedirect(referer != null ? referer : "/");
        };
    }

    @Bean
    public CorsConfigurationSource corsConfigurationSource() {
        CorsConfiguration config = new CorsConfiguration();
        config.addAllowedOrigin("*");
        config.setAllowedMethods(Arrays.asList("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS"));
        config.setAllowedHeaders(Arrays.asList("*"));
        config.setAllowCredentials(true);

        UrlBasedCorsConfigurationSource source = new UrlBasedCorsConfigurationSource();
        source.registerCorsConfiguration("/**", config);
        return source;

    }
}
