package com.sai.backend.domain.ai.service;

import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

@Service
public interface S3Service {
    String uploadImage(MultipartFile file);
}
