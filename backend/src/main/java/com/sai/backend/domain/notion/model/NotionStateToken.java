package com.sai.backend.domain.notion.model;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.annotation.Id;
import org.springframework.data.redis.core.RedisHash;
import org.springframework.data.redis.core.TimeToLive;
import org.springframework.data.redis.core.index.Indexed;

import java.time.LocalDateTime;
import java.util.concurrent.TimeUnit;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@RedisHash(value = "notion:state", timeToLive = 3600) // 1시간 TTL
public class NotionStateToken {

    @Id
    private String state;

    @Indexed  // UUID 필드에 인덱스 추가
    private String uuid;

    private LocalDateTime createdAt;

    private String redirectUri;

    @TimeToLive(unit = TimeUnit.SECONDS)
    private Long ttl;

    public static NotionStateToken create(String state, String uuid, String redirectUri) {
        return NotionStateToken.builder()
            .state(state)
            .uuid(uuid)
            .redirectUri(redirectUri)
            .createdAt(LocalDateTime.now())
            .ttl(3600L) // 1시간
            .build();
    }
}