package com.sai.backend.domain.admin.service;

import com.sai.backend.domain.admin.dto.request.AdminChangeRequestDto;
import com.sai.backend.domain.admin.dto.request.AdminLoginRequestDto;
import com.sai.backend.domain.admin.dto.request.AdminRegisterRequestDto;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpSession;
import org.springframework.stereotype.Service;

@Service
public interface AdminService {
    Boolean login(AdminLoginRequestDto adminLoginRequestDto, HttpServletRequest request);
    String ping(HttpSession session);
    boolean register(AdminRegisterRequestDto adminRegisterRequestDto);
    Boolean change(AdminChangeRequestDto adminChangeRequestDto);
}
