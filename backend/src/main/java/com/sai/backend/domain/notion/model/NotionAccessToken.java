package com.sai.backend.domain.notion.model;

import java.util.Map;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.annotation.Id;
import org.springframework.data.redis.core.RedisHash;
import org.springframework.data.redis.core.TimeToLive;

import java.time.LocalDateTime;
import java.util.concurrent.TimeUnit;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@RedisHash(value = "notion:token", timeToLive = 2592000) // 30일 TTL
public class NotionAccessToken {

    @Id
    private String uuid;

    private Map<String, Object> response;

    private String accessToken;

    private String duplicatedTemplateId;

    private String workspaceId;

    private LocalDateTime createdAt;

    private LocalDateTime expiresAt;

    @TimeToLive(unit = TimeUnit.SECONDS)
    private Long ttl;

    public static NotionAccessToken create(Map<String,Object> response, String uuid, String accessToken) {
        NotionAccessTokenBuilder builder = NotionAccessToken.builder()
            .uuid(uuid)
            .response(response)
            .accessToken(accessToken)
            .createdAt(LocalDateTime.now())
            .expiresAt(LocalDateTime.now().plusDays(30))
            .ttl(2592000L);

        if (response.containsKey("duplicated_template_id")) {
            builder.duplicatedTemplateId((String) response.get("duplicated_template_id"));
        }
        if (response.containsKey("workspace_id")) {
            builder.workspaceId((String) response.get("workspace_id"));
        }

        return builder.build();
    }

    public boolean isExpired() {
        return LocalDateTime.now().isAfter(expiresAt);
    }
}