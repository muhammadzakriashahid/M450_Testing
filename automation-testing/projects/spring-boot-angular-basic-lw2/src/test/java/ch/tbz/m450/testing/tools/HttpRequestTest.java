package ch.tbz.m450.testing.tools;

import ch.tbz.m450.testing.tools.repository.entities.Student;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.client.RestTemplate;

import static org.assertj.core.api.Assertions.assertThat;
import static org.junit.jupiter.api.Assertions.assertEquals;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class HttpRequestTest {

    @LocalServerPort
    private int port;

    @Autowired
    private RestTemplate restTemplate;

    @Test
    void getStudentsReturnIsNotEmpty() throws Exception {
        assertThat(this.restTemplate.getForObject("http://localhost:" + port + "/students", String.class)).isNotEmpty();
    }

    @Test
    void getStudentsReturnValueCheck() throws Exception {
        ResponseEntity<String> response = restTemplate.getForEntity("http://localhost:" + port + "/students", String.class);
        System.out.print(response.getBody());
        assertEquals(HttpStatus.OK, response.getStatusCode());
    }

    @Test
    void postStudentsReturnValueCheck() throws Exception {
        Student newStudent = new Student("Gugus", "gugus@gmail.it");
        ResponseEntity<Void> response = restTemplate.postForEntity("http://localhost:" + port + "/students", newStudent, Void.class);
        assertEquals(HttpStatus.OK, response.getStatusCode());

        ResponseEntity<String> getResponse = restTemplate.getForEntity("http://localhost:" + port + "/students", String.class);
        assertThat(getResponse.getBody()).contains("Gugus");
        assertThat(getResponse.getBody()).contains("gugus@gmail.it");
    }
}
