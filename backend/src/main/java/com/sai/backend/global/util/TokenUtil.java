package com.sai.backend.global.util;

import java.util.UUID;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;

@Component
@Slf4j
public class TokenUtil {

    public String generate() {
        return "sk-" + UUID.randomUUID().toString().replace("-", "");
    }
}
