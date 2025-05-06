package com.sai.backend.global.model.dto;

import lombok.Getter;
import net.minidev.json.annotate.JsonIgnore;
import org.springframework.http.HttpStatus;

@Getter
public enum BaseResponseStatus {
    SUCCESS(true, HttpStatus.OK, 200, "요청에 성공하였습니다."),
    AUTHORIZATION_SUCCESS(true, HttpStatus.OK, 200, "토큰 발급에 성공하였습니다."),
    BAD_REQUEST(false, HttpStatus.BAD_REQUEST, 400, "입력값을 확인해주세요."),
    UNAUTHORIZED(false, HttpStatus.UNAUTHORIZED, 401, "인증이 필요합니다."),
    FORBIDDEN(false, HttpStatus.FORBIDDEN, 403, "권한이 없습니다."),
    NOT_FOUND(false, HttpStatus.NOT_FOUND, 404, "대상을 찾을 수 없습니다."),

    // JWT (1001 ~ 1099)
    JWT_NOT_FOUND(false, HttpStatus.UNAUTHORIZED, 1001, "JWT를 찾을 수 없습니다."),
    JWT_EXPIRED(false, HttpStatus.UNAUTHORIZED, 1002, "만료된 JWT입니다."),
    JWT_INVALID(false, HttpStatus.UNAUTHORIZED, 1003, "유효하지 않은 JWT입니다."),
    REFRESH_TOKEN_INVALID(false, HttpStatus.UNAUTHORIZED, 1004, "유효하지 않은 Refresh Token입니다."),
    REFRESH_TOKEN_NOT_FOUND(false, HttpStatus.UNAUTHORIZED, 1005, "Refresh Token이 없습니다."),
    REFRESH_TOKEN_EXPIRED(false, HttpStatus.BAD_REQUEST, 1006, "만료된 Refresh Token입니다."),

    ;

    private final boolean isSuccess;
    @JsonIgnore
    private final HttpStatus httpStatus;
    private final int code;
    private final String message;

    BaseResponseStatus(boolean isSuccess, HttpStatus httpStatus, int code, String message) {
        this.isSuccess = isSuccess;
        this.httpStatus = httpStatus;
        this.code = code;
        this.message = message;
    }
}
