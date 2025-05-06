package com.sai.backend.domain.admin.controller;

import com.sai.backend.domain.admin.dto.request.AdminChangeRequestDto;
import com.sai.backend.domain.admin.dto.request.AdminLoginRequestDto;
import com.sai.backend.domain.admin.dto.request.AdminRegisterRequestDto;
import com.sai.backend.global.model.dto.BaseResponse;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpSession;
import org.springframework.http.ResponseEntity;
import org.springframework.web.HttpRequestHandler;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/api/admin")
public interface AdminApi {

    //로그인
    @PostMapping("/login")
    ResponseEntity<BaseResponse<Boolean>> login(@RequestBody AdminLoginRequestDto adminRequestDto,
        HttpServletRequest request);

    @GetMapping("/ping")
    ResponseEntity<BaseResponse<String>> ping(HttpSession session);

    //아래는 공개되면 안 되는 api
    //어드민 등록
    @PostMapping("/register")
    ResponseEntity<BaseResponse<Boolean>> register(@RequestBody AdminRegisterRequestDto adminRegisterRequestDto);

    //어드민 비밀번호 변경
    @PostMapping("/change")
    ResponseEntity<BaseResponse<Boolean>> change(@RequestBody AdminChangeRequestDto adminChangeRequestDto);
}
