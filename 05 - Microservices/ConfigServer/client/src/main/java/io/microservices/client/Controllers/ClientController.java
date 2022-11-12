package io.microservices.client.Controllers;

import org.springframework.boot.web.client.RestTemplateBuilder;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

@RestController
public class ClientController {
    
    private RestTemplateBuilder _restTemplateBuilder;

    public ClientController(RestTemplateBuilder restTemplateBuilder) {        
        super();        
        _restTemplateBuilder = restTemplateBuilder;
    }

    @RequestMapping("/")
    public String CallService(){
        final String uri = "http://localhost:8082/";
        RestTemplate restTemplate = _restTemplateBuilder.build();
        ResponseEntity<String> response = restTemplate.exchange(uri,HttpMethod.GET,null,String.class);
        
        return response.getBody();
    }
}
