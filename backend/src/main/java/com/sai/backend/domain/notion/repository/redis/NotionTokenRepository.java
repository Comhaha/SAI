package com.sai.backend.domain.notion.repository.redis;

import com.sai.backend.domain.notion.model.NotionAccessToken;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface NotionTokenRepository extends CrudRepository<NotionAccessToken, String> {

    Optional<NotionAccessToken> findByUuid(String uuid);

    void deleteByUuid(String uuid);
}