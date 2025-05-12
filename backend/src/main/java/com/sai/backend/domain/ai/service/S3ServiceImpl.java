package com.sai.backend.domain.ai.service;

import java.io.IOException;
import java.net.URL;
import java.time.Duration;
import java.util.UUID;

import lombok.RequiredArgsConstructor;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;
import software.amazon.awssdk.core.sync.RequestBody;
import software.amazon.awssdk.services.s3.S3Client;
import software.amazon.awssdk.services.s3.model.GetObjectRequest;
import software.amazon.awssdk.services.s3.model.PutObjectRequest;
import software.amazon.awssdk.services.s3.presigner.S3Presigner;
import software.amazon.awssdk.services.s3.presigner.model.GetObjectPresignRequest;

@Service
@RequiredArgsConstructor
public class S3ServiceImpl implements S3Service {

    private static final long MAX_SIZE = 4_000_000;          // 4 MB
    private static final Duration URL_TTL = Duration.ofMinutes(10);

    private final S3Client s3Client;
    private final S3Presigner s3Presigner;

    @Value("${cloud.aws.s3.bucket}")
    private String bucket;


    @Override
    public String uploadImage(MultipartFile file) {
        if (file.getSize() > MAX_SIZE)
            throw new IllegalArgumentException("이미지 용량이 4 MB를 초과합니다.");

        String key = "ai-vision/" + UUID.randomUUID() + ext(file.getOriginalFilename());

        try {
            s3Client.putObject(
                PutObjectRequest.builder()
                    .bucket(bucket)
                    .key(key)
                    .contentType(file.getContentType())
                    .build(),
                RequestBody.fromBytes(file.getBytes())
            );
        } catch (IOException e) {
            throw new RuntimeException("S3 업로드 실패", e);
        }

        // presigned GET URL
        GetObjectRequest getReq = GetObjectRequest.builder()
            .bucket(bucket)
            .key(key)
            .build();

        URL url = s3Presigner.presignGetObject(
            GetObjectPresignRequest.builder()
                .signatureDuration(URL_TTL)
                .getObjectRequest(getReq)
                .build()
        ).url();

        return url.toString();
    }

    private String ext(String name){
        int idx = name != null ? name.lastIndexOf('.') : -1;
        return (idx>-1)? name.substring(idx) : "";
    }
}
