package io.microservices.client.Controllers;

import org.springframework.boot.web.client.RestTemplateBuilder;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

import com.netflix.appinfo.InstanceInfo;
import com.netflix.discovery.EurekaClient;

@RestController
public class ClientController {
    
    private RestTemplateBuilder _restTemplateBuilder;
    private EurekaClient _client;

    public ClientController(
        RestTemplateBuilder restTemplateBuilder,
        EurekaClient client) {        
        super();        
        _restTemplateBuilder = restTemplateBuilder;
        _client = client;
    }

    @RequestMapping("/")
    public String CallService(){
        
        InstanceInfo instanceInfo =  _client.getNextServerFromEureka("SERVICE", false);
        String baseUrl = instanceInfo.getHomePageUrl();

        //final String uri = "http://localhost:8082/";
        RestTemplate restTemplate = _restTemplateBuilder.build();
        ResponseEntity<String> response = restTemplate.exchange(baseUrl,HttpMethod.GET,null,String.class);
        
        return response.getBody();
    }
}
