package com.sai.backend.domain.token.model;

import jakarta.persistence.Column;
import jakarta.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.redis.core.RedisHash;
import org.springframework.data.redis.core.TimeToLive;

@RedisHash("current-token")
@Data
@AllArgsConstructor
@NoArgsConstructor
public class RedisToken {

    @Id
    private String id = "singleton-current-token";

    private String token;
    private Long useCount;

    @TimeToLive
    private Long expiration = 86_400L;

    public Token toEntity() {
        return Token.builder()
            .token(token)
            .useCount(useCount)
            .build();
    }
}
