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
    public Boolean login(AdminLoginRequestDto dto, HttpServletRequest request) {
        Admin admin = adminRepository.findByAdminId(ADMIN).orElse(null);

        if(admin == null) return false;

        if(!passwordEncoder.matches(dto.getPassword(), admin.getPassword())) return false;

        Authentication auth = new UsernamePasswordAuthenticationToken(
            ADMIN, null,
            java.util.List.of(new SimpleGrantedAuthority("ADMIN"))
        );

        SecurityContextHolder.getContext().setAuthentication(auth);

        HttpSession session = request.getSession(true);
        session.setMaxInactiveInterval(300);
        session.setAttribute(HttpSessionSecurityContextRepository.SPRING_SECURITY_CONTEXT_KEY,
            SecurityContextHolder.getContext());

        return true;
    }

    @Override
    @Transactional(readOnly = true)
    public String ping(HttpSession session) {
        return Optional.ofNullable(session)
            .map(HttpSession::getId)
            .map(id -> "세션 유지 중 (" + id + ")")
            .orElse("세션이 없습니다.");
    }

    @Override
    public boolean register(AdminRegisterRequestDto dto) {
        if(!dto.getPassword().equals(dto.getPassword2())) {
            return false;
        }

        dto.setPassword(passwordEncoder.encode(dto.getPassword()));
        Admin admin = dto.toEntity();
        admin.setAdminId(ADMIN);
        adminRepository.save(admin);

        return true;
    }

    @Override
    public Boolean change(AdminChangeRequestDto dto) {
        Admin admin = adminRepository.findByAdminId(ADMIN).orElse(null);

        if(admin == null) return false;

        if(!passwordEncoder.matches(dto.getCurrentPw(), admin.getPassword())) return false;

        admin.setPassword(passwordEncoder.encode(dto.getNewPw()));
        adminRepository.save(admin);

        return true;
    }
}
