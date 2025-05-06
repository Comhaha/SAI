package com.sai.backend.domain.download.controller;

import com.sai.backend.global.model.dto.BaseResponse;
import java.net.URL;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;

@RequestMapping("/api/download")
public interface DownloadApi {

    @GetMapping
    ResponseEntity<BaseResponse<URL>> redirectToPresignedUrl();
}
