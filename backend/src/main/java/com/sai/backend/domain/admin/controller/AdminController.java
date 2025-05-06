package com.sai.backend.domain.admin.controller;

import com.sai.backend.domain.admin.dto.request.AdminChangeRequestDto;
import com.sai.backend.domain.admin.dto.request.AdminLoginRequestDto;
import com.sai.backend.domain.admin.dto.request.AdminRegisterRequestDto;
import com.sai.backend.domain.admin.service.AdminService;
import com.sai.backend.global.model.dto.BaseResponse;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpSession;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequiredArgsConstructor
@Slf4j
public class AdminController implements AdminApi {

    private final AdminService adminService;

    @Override
    public ResponseEntity<BaseResponse<Boolean>> login(AdminLoginRequestDto adminLoginRequestDto,
        HttpServletRequest request) {
        return ResponseEntity.ok(new BaseResponse<>(adminService.login(adminLoginRequestDto, request)));
    }

    @Override
    public ResponseEntity<BaseResponse<String>> ping(HttpSession session) {
        return ResponseEntity.ok(new BaseResponse<>(adminService.ping(session)));
    }

    @Override
    public ResponseEntity<BaseResponse<Boolean>> register(AdminRegisterRequestDto adminRegisterRequestDto) {
        return ResponseEntity.ok(new BaseResponse<>(adminService.register(adminRegisterRequestDto)));
    }

    @Override
    public ResponseEntity<BaseResponse<Boolean>> change(AdminChangeRequestDto adminChangeRequestDto) {
        return ResponseEntity.ok(new BaseResponse<>(adminService.change(adminChangeRequestDto)));
    }
}
