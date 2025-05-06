package com.sai.backend.domain.token.repository.redis;

import com.sai.backend.domain.token.model.RedisToken;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface RedisTokenRepository extends CrudRepository<RedisToken, String> {

}

