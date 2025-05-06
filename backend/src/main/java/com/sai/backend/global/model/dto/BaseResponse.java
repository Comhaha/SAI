package com.sai.backend.global.model.dto;

import static com.sai.backend.global.model.dto.BaseResponseStatus.SUCCESS;

import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonPropertyOrder;
import io.swagger.v3.oas.annotations.media.Schema;
import java.util.ArrayList;
import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
@JsonPropertyOrder({"isSuccess", "code", "message", "result"})
@Schema(hidden = true)
public class BaseResponse<T> {//BaseResponse 객체를 사용할때 성공, 실패 경우

    @JsonProperty("isSuccess")
    @Schema(
        description = "요청 성공 여부",
        example = "true"
    )
    private final Boolean isSuccess;

    @Schema(
        description = "응답 메시지",
        example = "요청에 성공하였습니다."
    )
    private final String message;

    @Schema(
        description = "HTTP 응답 코드",
        example = "200"
    )
    private final int code;

    @JsonInclude(JsonInclude.Include.NON_NULL)
    @Schema(
        description = "응답 데이터 객체 (실제 응답 내용)"
    )
    private T result;

    // 기본 성공 응답 (결과값 포함)
    public BaseResponse(T result) {
        this.isSuccess = SUCCESS.isSuccess();
        this.message = SUCCESS.getMessage();
        this.code = SUCCESS.getCode();
        this.result = result;
    }

    // 요청에 실패한 경우
    public BaseResponse(BaseResponseStatus status) {
        this.isSuccess = status.isSuccess();
        this.message = status.getMessage();
        this.code = status.getCode();
    }

    // 빈 목록 반환
    public BaseResponse() {
        this.isSuccess = SUCCESS.isSuccess();
        this.message = SUCCESS.getMessage();
        this.code = SUCCESS.getCode();
        this.result = (T) new ArrayList<>();
    }
}

