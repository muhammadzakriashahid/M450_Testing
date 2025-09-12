package ch.tbz.m450.controller;

import java.util.Date;
import java.util.List;
import java.util.Optional;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.springframework.http.ResponseEntity;

import ch.tbz.m450.repository.Address;
import ch.tbz.m450.service.AddressService;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.BDDMockito.given;

// register Mockito extension for using Mockito annotations
@ExtendWith(MockitoExtension.class)
class AddressControllerTest {

    // create an instance of AddressController and inject the mocks into it
    @InjectMocks
    private AddressController addressController;

    // add a mock of the AddressService
    @Mock
    private AddressService addressService;

    @Test
    void createAddress() {
        // Arrange
        Address address = new Address();
        address.setId(1);
        address.setFirstname("Gugus");
        address.setLastname("Foobar");
        address.setPhonenumber("0123456789");
        address.setRegistrationDate(new Date());

        // Act: Stub the service's save method (not the controller's createAddress)
        given(addressService.save(any(Address.class))).willReturn(address);

        ResponseEntity<Address> response = addressController.createAddress(address);
        Address newAddress = response.getBody();

        // Assert
        assertEquals(address.getId(), newAddress.getId());
        assertEquals(address.getFirstname(), newAddress.getFirstname());
        assertEquals(address.getLastname(), newAddress.getLastname());
        assertEquals(address.getPhonenumber(), newAddress.getPhonenumber());
        assertEquals(address.getRegistrationDate(), newAddress.getRegistrationDate());
    }

    @Test
    void getAddresses() {
        // Arrange
        Address address1 = new Address(1, "John", "Doe", "123", new Date());
        Address address2 = new Address(2, "Jane", "Smith", "456", new Date());
        List<Address> addresses = List.of(address1, address2);
        
        // Act: Stub the service's getAll method
        given(addressService.getAll()).willReturn(addresses);
        
        ResponseEntity<List<Address>> response = addressController.getAddresses();
        List<Address> result = response.getBody();
        
        // Assert
        assertEquals(2, result.size());
        assertEquals("John", result.get(0).getFirstname());
    }

    @Test
    void getAddress() {
        // Arrange
        Address address = new Address(1, "Thomas", "Müller", "123", new Date());
        
        // Act: Stub the service's getAddress method for found case
        given(addressService.getAddress(1)).willReturn(Optional.of(address));
        
        ResponseEntity<Address> response = addressController.getAddress(1);
        Address result = response.getBody();
        
        // Assert
        assertEquals("Thomas", result.getFirstname());
        assertEquals("Müller", result.getLastname());
        assertEquals("123", result.getPhonenumber());
    }
}