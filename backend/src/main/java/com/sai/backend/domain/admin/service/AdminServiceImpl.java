package com.sai.backend.domain.admin.service;

import com.sai.backend.domain.admin.dto.request.AdminChangeRequestDto;
import com.sai.backend.domain.admin.dto.request.AdminLoginRequestDto;
import com.sai.backend.domain.admin.dto.request.AdminRegisterRequestDto;
import com.sai.backend.domain.admin.model.Admin;
import com.sai.backend.domain.admin.repository.AdminRepository;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpSession;
import java.util.Optional;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.context.HttpSessionSecurityContextRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@RequiredArgsConstructor
@Slf4j
public class AdminServiceImpl implements AdminService {

    private static final String ADMIN = "admin";
    private final AdminRepository adminRepository;
    private final PasswordEncoder passwordEncoder;

    @Override
    @Transactional
    public Boolean login(AdminLoginRequestDto dto, HttpServletRequest request) {
        try {
            Admin admin = adminRepository.findByAdminId(ADMIN).orElse(null);

            if (admin == null) {
                log.warn("Admin user not found");
                return false;
            }

            if (!passwordEncoder.matches(dto.getPassword(), admin.getPassword())) {
                log.warn("Password mismatch for admin login");
                return false;
            }

            // 기존 세션 무효화 (있다면)
            HttpSession existingSession = request.getSession(false);
            if (existingSession != null) {
                log.info("Invalidating existing session: {}", existingSession.getId());
                existingSession.invalidate();
                SecurityContextHolder.clearContext();
            }

            // 새 세션 생성
            HttpSession newSession = request.getSession(true);
            request.changeSessionId();
            newSession.setMaxInactiveInterval(300); // 5분 (300초)

            // 인증 객체 생성 - ADMIN 권한 부여
            Authentication auth = new UsernamePasswordAuthenticationToken(
                ADMIN, null,
                java.util.List.of(new SimpleGrantedAuthority("ADMIN"))
            );

            SecurityContextHolder.getContext().setAuthentication(auth);

            // 세션에 SecurityContext 저장
            newSession.setAttribute(HttpSessionSecurityContextRepository.SPRING_SECURITY_CONTEXT_KEY,
                SecurityContextHolder.getContext());

            log.info("Admin login successful, new session created: {} with 5-minute timeout", newSession.getId());
            return true;

        } catch (Exception e) {
            log.error("Admin login error", e);
            return false;
        }
    }

    @Override
    @Transactional(readOnly = true)
    public String ping(HttpSession session) {
        if (session == null) {
            return "세션이 없습니다.";
        }

        // 세션 유효성 검사
        try {
            String sessionId = session.getId();
            int maxInactiveInterval = session.getMaxInactiveInterval();
            long lastAccessedTime = session.getLastAccessedTime();
            long currentTime = System.currentTimeMillis();
            long remainingTime = maxInactiveInterval * 1000L - (currentTime - lastAccessedTime);

            if (remainingTime <= 0) {
                log.info("세션이 만료되었습니다.");
                return "세션이 만료되었습니다.";
            }

            long remainingMinutes = remainingTime / (60 * 1000);
            long remainingSeconds = (remainingTime % (60 * 1000)) / 1000;

            log.info("세션 유지 중 (ID: {}, 남은 시간: {}분 {}초)", sessionId, remainingMinutes, remainingSeconds);
            return String.format("세션 유지 중 (ID: %s, 남은 시간: %d분 %d초)",
                sessionId, remainingMinutes, remainingSeconds);

        } catch (Exception e) {
            log.error("Session ping error", e);
            return "세션 오류가 발생했습니다.";
        }
    }

    @Override
    @Transactional
    public boolean register(AdminRegisterRequestDto dto) {
        try {
            if (!dto.getPassword().equals(dto.getPassword2())) {
                log.warn("Password confirmation mismatch during admin registration");
                return false;
            }

            // 기존 admin 확인
            Optional<Admin> existingAdmin = adminRepository.findByAdminId(ADMIN);
            if (existingAdmin.isPresent()) {
                log.warn("Admin user already exists");
                return false;
            }

            dto.setPassword(passwordEncoder.encode(dto.getPassword()));
            Admin admin = dto.toEntity();
            admin.setAdminId(ADMIN);
            adminRepository.save(admin);

            log.info("Admin user registered successfully");
            return true;

        } catch (Exception e) {
            log.error("Admin registration error", e);
            return false;
        }
    }

    @Override
    @Transactional
    public Boolean change(AdminChangeRequestDto dto) {
        try {
            Admin admin = adminRepository.findByAdminId(ADMIN).orElse(null);

            if (admin == null) {
                log.warn("Admin user not found during password change");
                return false;
            }

            if (!passwordEncoder.matches(dto.getCurrentPw(), admin.getPassword())) {
                log.warn("Current password mismatch during password change");
                return false;
            }

            admin.setPassword(passwordEncoder.encode(dto.getNewPw()));
            adminRepository.save(admin);

            log.info("Admin password changed successfully");
            return true;

        } catch (Exception e) {
            log.error("Admin password change error", e);
            return false;
        }
    }
}