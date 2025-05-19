package com.sai.backend.domain.ai.model;

import jakarta.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;
import org.springframework.data.mongodb.core.mapping.Document;

@Getter
@Setter
@AllArgsConstructor
@Document(collection="AiLog")
public class AiLog {
    @Id
    private String id;
    private String code;
    private String logImageUrl;
    private String resultImageUrl;
    private String memo;
    private String feedback;
}
