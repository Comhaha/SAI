package com.sai.backend.domain.download.service;

import java.net.URL;
import java.time.Duration;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import software.amazon.awssdk.services.s3.S3Client;
import software.amazon.awssdk.services.s3.model.GetObjectRequest;
import software.amazon.awssdk.services.s3.presigner.S3Presigner;
import software.amazon.awssdk.services.s3.presigner.model.GetObjectPresignRequest;

@Service
@RequiredArgsConstructor
public class S3DownLoadServiceImpl implements S3DownloadService{

    private static final Duration DOWNLOAD_DURATION = Duration.ofSeconds(30);

    private final S3Presigner s3Presigner;

    @Value("${cloud.aws.s3.bucket}")
    private String bucketName;

    @Override
    public URL generatePresignedUrl() {
        GetObjectRequest get = GetObjectRequest.builder()
            .bucket(bucketName)
            .key("SAISetup.exe")
            .build();

        GetObjectPresignRequest presign = GetObjectPresignRequest.builder()
            .signatureDuration(DOWNLOAD_DURATION)
            .getObjectRequest(get)
            .build();

        return  s3Presigner.presignGetObject(presign).url();
    }
}
