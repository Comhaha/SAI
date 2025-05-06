package com.sai.backend.global.model.enums;

import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public enum PublicEndpoint {
    RENDING("/"),
    DOWNLOAD("/download/**"),

    ;

    private String url;

    public static List<String> getAll(){
        return List.of(values()).stream().map(PublicEndpoint::getUrl).toList();
    }
}
