package com.sai.backend.domain.download.controller;

import com.sai.backend.domain.download.service.S3DownloadService;
import com.sai.backend.global.model.dto.BaseResponse;
import java.net.URL;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequiredArgsConstructor
public class DownloadController implements DownloadApi{

    private final S3DownloadService s3DownloadService;

    @Override
    public ResponseEntity<BaseResponse<URL>> redirectToPresignedUrl() {
        return ResponseEntity.ok(new BaseResponse<>(s3DownloadService.generatePresignedUrl()));
    }

    @Override
    public ResponseEntity<BaseResponse<URL>> redirectTutorialDataToPresignedUrl() {
        return ResponseEntity.ok(new BaseResponse<>(s3DownloadService.generateTutorialDataPresignedUrl()));
    }

    @Override
    public ResponseEntity<BaseResponse<URL>> redirectPracticeDataToPresignedUrl() {
        return ResponseEntity.ok(new BaseResponse<>(s3DownloadService.generatePracticePresignedUrl()));
    }
}
