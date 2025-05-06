package com.sai.backend.domain.token.repository.jpa;

import com.sai.backend.domain.token.model.Token;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface JpaTokenRepository extends JpaRepository<Token, Long> {

}
