package io.microservices.configclientapp.Controllers;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.cloud.context.config.annotation.RefreshScope;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import io.microservices.configclientapp.Models.ConfigClientAppConfiguration;

@RestController
@EnableConfigurationProperties
@RefreshScope
public class ConfigClientAppController {
    
    @Value("${some.other.property}")
    private String someOtherProperty;

    private ConfigClientAppConfiguration _properties;
    
    public ConfigClientAppController(ConfigClientAppConfiguration properties) {
        super();
        _properties = properties;
    }

    @RequestMapping("/")
    public String printConfig(){
        StringBuilder sb = new StringBuilder();
        sb.append(_properties.getProperty());
        sb.append(" || ");
        sb.append(someOtherProperty);

        return sb.toString();
    }
}
