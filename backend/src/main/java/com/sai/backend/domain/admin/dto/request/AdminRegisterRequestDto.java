package com.sai.backend.domain.admin.dto.request;

import com.sai.backend.domain.admin.model.Admin;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class AdminRegisterRequestDto {
    private String password;
    private String password2;

    public Admin toEntity(){
        return Admin.builder()
            .password(password)
            .build();
    }
}
