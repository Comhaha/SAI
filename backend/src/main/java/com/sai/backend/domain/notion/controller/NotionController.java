package com.sai.backend.domain.notion.controller;

import com.sai.backend.domain.notion.dto.request.CallbackRequestDto;
import com.sai.backend.domain.notion.dto.request.ExportRequestDto;
import com.sai.backend.domain.notion.dto.response.AuthCheckResponseDto;
import com.sai.backend.domain.notion.dto.response.ExportResponseDto;
import com.sai.backend.domain.notion.service.NotionService;
import com.sai.backend.global.model.dto.BaseResponse;
import java.util.Map;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequiredArgsConstructor
@Slf4j
public class NotionController implements NotionApi {

    private final NotionService notionService;

//    @Override
//    public ResponseEntity<BaseResponse<AuthCheckResponseDto>> checkAuthStatus(String uuid) {
//        log.info("Checking auth status for UUID: {}", uuid);
//
//        AuthCheckResponseDto authStatus = notionService.checkAuthStatus(uuid);
//        return ResponseEntity.ok(new BaseResponse<>(authStatus));
//    }
//
//    @Override
//    public ResponseEntity<BaseResponse<ExportResponseDto>> exportReport(ExportRequestDto requestDto) {
//        log.info("Export request for UUID: {}", requestDto.getUuid());
//
//        // 인증 상태 확인
//        AuthCheckResponseDto authStatus = notionService.checkAuthStatus(requestDto.getUuid());
//
//        if (!authStatus.getAuthenticated()) {
//            // 인증되지 않은 경우
//            ExportResponseDto response = ExportResponseDto.builder()
//                .success(false)
//                .message("인증이 필요합니다. 먼저 Notion 인증을 진행해주세요.")
//                .build();
//            return ResponseEntity.badRequest().body(new BaseResponse<>(response));
//        }
//
//        // 인증된 경우 내보내기 실행
//        ExportResponseDto result = notionService.exportToNotion(requestDto.getUuid());
//
//        if (result.getSuccess()) {
//            return ResponseEntity.ok(new BaseResponse<>(result));
//        } else {
//            return ResponseEntity.badRequest().body(new BaseResponse<>(result));
//        }
//    }

//    @Override
//    public ResponseEntity<BaseResponse<String>> handleCallback(String code, String state) {
//        log.info("Controller callback - code: {}, state: {}", code, state);
//
//        CallbackRequestDto dto = new CallbackRequestDto();
//        dto.setCode(code);
//        dto.setState(state);
//
//        String result = notionService.handleOAuthCallback(dto);
//        return ResponseEntity.ok(new BaseResponse<>(result));
//    }

    @Override
    public ResponseEntity<BaseResponse<ExportResponseDto>> handleCallback(String code, String state) {
        log.info("Controller callback - code: {}, state: {}", code, state);

        CallbackRequestDto dto = new CallbackRequestDto();
        dto.setCode(code);
        dto.setState(state);

        ExportResponseDto result = notionService.handleOAuthCallback(dto);
        return ResponseEntity.ok(new BaseResponse<>(result));
    }
}
