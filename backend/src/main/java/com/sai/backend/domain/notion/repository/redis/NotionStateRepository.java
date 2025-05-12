package com.sai.backend.domain.notion.repository.redis;

import com.sai.backend.domain.notion.model.NotionStateToken;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface NotionStateRepository extends CrudRepository<NotionStateToken, String> {

    Optional<NotionStateToken> findByState(String state);

    Optional<NotionStateToken> findByUuid(String uuid);

    void deleteByState(String state);
}