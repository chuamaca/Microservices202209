package io.microservices.configclientapp.Models;

import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

@Component
@ConfigurationProperties(prefix="some")
public class ConfigClientAppConfiguration {
    private String _property;

    public String getProperty(){
        return _property;
    }
    public void setProperty(String property){
        _property = property;
    }
}
