package com.sai.backend.global.config;

import com.sai.backend.domain.token.repository.redis.RedisTokenRepository;
import com.sai.backend.global.filter.ApiTokenFilter;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import jakarta.servlet.http.HttpSession;
import java.io.IOException;
import java.util.List;
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
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.AuthenticationFailureHandler;
import org.springframework.security.web.authentication.AuthenticationSuccessHandler;
import org.springframework.security.web.authentication.logout.LogoutSuccessHandler;
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

        http.securityMatcher("/api/admin/**", "/api/token/**")
            .csrf(AbstractHttpConfigurer::disable)
            .cors(Customizer.withDefaults())
            .sessionManagement(s -> s
                .sessionCreationPolicy(SessionCreationPolicy.IF_REQUIRED)
                .sessionFixation().migrateSession()
                .invalidSessionStrategy(customInvalidSessionStrategy())
                .maximumSessions(1)
                .maxSessionsPreventsLogin(false)
                .expiredSessionStrategy(event -> {
                    HttpServletResponse response = event.getResponse();
                    HttpServletRequest request = event.getRequest();

                    // 기존 세션 완전 삭제
                    HttpSession session = request.getSession(false);
                    if (session != null) {
                        session.invalidate();
                    }

                    // SecurityContext 클리어
                    SecurityContextHolder.clearContext();

                    try {
                        response.setStatus(HttpServletResponse.SC_UNAUTHORIZED);
                        response.setContentType("application/json;charset=UTF-8");
                        response.getWriter().write(
                            "{\"isSuccess\":false,\"message\":\"세션이 만료되었습니다. 다시 로그인해주세요.\",\"code\":401,\"needLogin\":true}"
                        );
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                })
            )
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/api/admin/login", "/api/admin/register", "/api/admin/change").permitAll()
                .requestMatchers("/api/admin/**", "/api/token/**").hasAuthority("ADMIN")
                .anyRequest().authenticated()
            )
            .formLogin(form -> form
                .usernameParameter("password")
                .passwordParameter("password")
                .successHandler(customAuthenticationSuccessHandler())
                .failureHandler(customAuthenticationFailureHandler())
                .permitAll()
            )
            .logout(logout -> logout
                .logoutUrl("/api/admin/logout")
                .logoutSuccessHandler(customLogoutSuccessHandler())
                .invalidateHttpSession(true)
                .deleteCookies("JSESSIONID")
                .permitAll()
            )
            .exceptionHandling(ex -> ex
                .authenticationEntryPoint((request, response, authException) -> {
                    // 세션이 만료되었거나 인증이 필요한 경우
                    response.setStatus(HttpServletResponse.SC_UNAUTHORIZED);
                    response.setContentType("application/json;charset=UTF-8");
                    response.getWriter().write(
                        "{\"isSuccess\":false,\"message\":\"로그인이 필요합니다.\",\"code\":401,\"needLogin\":true}"
                    );
                })
                .accessDeniedHandler((request, response, accessDeniedException) -> {
                    // 권한이 부족한 경우
                    response.setStatus(HttpServletResponse.SC_FORBIDDEN);
                    response.setContentType("application/json;charset=UTF-8");
                    response.getWriter().write(
                        "{\"isSuccess\":false,\"message\":\"권한이 부족합니다.\",\"code\":403}"
                    );
                })
            );

        return http.build();
    }

    /*───────────────── ②  나머지 API (stateless) ─────────────────*/
    @Bean
    @Order(2)
    public SecurityFilterChain apiChain(HttpSecurity http) throws Exception {

        ApiTokenFilter apiTokenFilter =
            new ApiTokenFilter(redisTokenRepository, redisTemplate);

        http.csrf(AbstractHttpConfigurer::disable)
            .cors(Customizer.withDefaults())
            .sessionManagement(s -> s.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/api/download/**", "/api/notion/**").permitAll()
                .requestMatchers("/api/ai/**").authenticated()
                .anyRequest().permitAll())
            .addFilterBefore(apiTokenFilter,
                org.springframework.security.web.authentication.
                    UsernamePasswordAuthenticationFilter.class);

        return http.build();
    }

    @Bean
    public InvalidSessionStrategy customInvalidSessionStrategy() {
        return new InvalidSessionStrategy() {
            @Override
            public void onInvalidSessionDetected(HttpServletRequest request, HttpServletResponse response)
                throws IOException, ServletException {

                // 세션 무효화 시 완전히 정리
                HttpSession session = request.getSession(false);
                if (session != null) {
                    session.invalidate();
                }

                // SecurityContext 완전히 클리어
                SecurityContextHolder.clearContext();

                // /api/admin/login 요청인 경우는 세션 만료 응답을 하지 않고 통과
                if ("/api/admin/login".equals(request.getRequestURI())) {
                    // 로그인 요청은 세션이 없어도 정상 처리되어야 함
                    return;
                }

                // 세션이 무효화되었을 때의 처리
                response.setStatus(HttpServletResponse.SC_UNAUTHORIZED);
                response.setContentType("application/json;charset=UTF-8");
                response.getWriter().write(
                    "{\"isSuccess\":false,\"message\":\"세션이 만료되었습니다. 다시 로그인해주세요.\",\"code\":401,\"needLogin\":true}"
                );
            }
        };
    }

    @Bean
    public AuthenticationSuccessHandler customAuthenticationSuccessHandler() {
        return new AuthenticationSuccessHandler() {
            @Override
            public void onAuthenticationSuccess(HttpServletRequest request, HttpServletResponse response,
                org.springframework.security.core.Authentication authentication)
                throws IOException, ServletException {

                // 로그인 성공 시 세션 타임아웃 설정 (5분)
                HttpSession session = request.getSession();
                session.setMaxInactiveInterval(300); // 5분

                response.setStatus(HttpServletResponse.SC_OK);
                response.setContentType("application/json;charset=UTF-8");
                response.getWriter().write(
                    "{\"isSuccess\":true,\"message\":\"로그인 성공\",\"code\":200,\"result\":true}"
                );
            }
        };
    }

    @Bean
    public AuthenticationFailureHandler customAuthenticationFailureHandler() {
        return new AuthenticationFailureHandler() {
            @Override
            public void onAuthenticationFailure(HttpServletRequest request, HttpServletResponse response,
                org.springframework.security.core.AuthenticationException exception)
                throws IOException, ServletException {

                response.setStatus(HttpServletResponse.SC_UNAUTHORIZED);
                response.setContentType("application/json;charset=UTF-8");
                response.getWriter().write(
                    "{\"isSuccess\":false,\"message\":\"로그인 실패\",\"code\":401,\"result\":false}"
                );
            }
        };
    }

    @Bean
    public LogoutSuccessHandler customLogoutSuccessHandler() {
        return new LogoutSuccessHandler() {
            @Override
            public void onLogoutSuccess(HttpServletRequest request, HttpServletResponse response,
                org.springframework.security.core.Authentication authentication)
                throws IOException, ServletException {

                response.setStatus(HttpServletResponse.SC_OK);
                response.setContentType("application/json;charset=UTF-8");
                response.getWriter().write(
                    "{\"isSuccess\":true,\"message\":\"로그아웃 성공\",\"code\":200}"
                );
            }
        };
    }

    @Bean
    public CorsConfigurationSource corsConfigurationSource() {
        CorsConfiguration config = new CorsConfiguration();
        config.setAllowedOrigins(List.of("https://k12d201.p.ssafy.io", "http://localhost:3000"));
        config.setAllowCredentials(true);
        config.addAllowedHeader("*");
        config.addAllowedMethod("*");

        UrlBasedCorsConfigurationSource source = new UrlBasedCorsConfigurationSource();
        source.registerCorsConfiguration("/**", config);
        return source;
    }
}