package com.sai.backend.domain.token.model;

import com.sai.backend.global.model.entity.BaseEntity;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@Table(name = "token")
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class Token extends BaseEntity {

    @Id
    @GeneratedValue()
    @Column(name = "token_id")
    private Long tokenId;

    @Column(name = "token")
    private String token;

    @Column(name = "use_count")
    private Long useCount;
}
