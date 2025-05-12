package com.sai.backend.global.config;

import java.time.Duration;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.MediaType;
import org.springframework.web.reactive.function.client.ExchangeStrategies;
import org.springframework.web.reactive.function.client.WebClient;

@Configuration
public class WebClientConfig {

    @Value("${openai.api-url}")
    private String openAiApiUrl;

    @Value("${openai.api-key}")
    private String openAiApiKey;

    @Bean
    public WebClient openAiWebClient() {

        // 20 MB까지 버퍼(이미지 base64 포함 시 여유 있게)
        ExchangeStrategies strategies = ExchangeStrategies.builder()
            .codecs(cfg -> cfg.defaultCodecs().maxInMemorySize(20 * 1024 * 1024))
            .build();

        return WebClient.builder()
            .baseUrl(openAiApiUrl)
            .defaultHeader("Authorization", "Bearer " + openAiApiKey)
            .defaultHeader("Content-Type", MediaType.APPLICATION_JSON_VALUE)
            .exchangeStrategies(strategies)
            .build();
    }

    @Value("${notion.api.url}")
    private String notionApiUrl;

    @Value("${notion.api.version}")
    private String notionVersion;

    @Bean
    public WebClient notionWebClient() {
        ExchangeStrategies strategies = ExchangeStrategies.builder()
            .codecs(cfg -> cfg.defaultCodecs().maxInMemorySize(5 * 1024 * 1024))
            .build();

        return WebClient.builder()
            .baseUrl(notionApiUrl)
            .defaultHeader("Notion-Version", notionVersion)
            .defaultHeader("Content-Type", MediaType.APPLICATION_JSON_VALUE)
            .exchangeStrategies(strategies)
            // 타임아웃 설정 - Notion API는 가끔 느릴 수 있음
            .codecs(configurer -> configurer.defaultCodecs().maxInMemorySize(5 * 1024 * 1024))
            .build();
    }


}
