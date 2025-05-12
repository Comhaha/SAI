package com.sai.backend.domain.ai.repository.mongo;

import com.sai.backend.domain.ai.model.AiLog;
import java.util.Optional;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface AiFeedbackRepository extends MongoRepository<AiLog, Long> {

    Optional<AiLog> findById(String id);
}
