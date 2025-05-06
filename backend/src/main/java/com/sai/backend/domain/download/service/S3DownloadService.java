package com.sai.backend.domain.download.service;

import java.net.URL;
import org.springframework.stereotype.Service;

@Service
public interface S3DownloadService {
    URL generatePresignedUrl();
}
