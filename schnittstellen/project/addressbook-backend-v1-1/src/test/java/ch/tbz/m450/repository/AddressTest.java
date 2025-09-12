package ch.tbz.m450.repository;

import java.util.Date;

import org.junit.jupiter.api.Test;

class AddressTest {
    @Test
    public void testAddressGettersAndSetters() {
        // Arrange
        Address address = new Address();
        int id = 7;
        String firstname = "Joe";
        String lastname = "Mama";
        String phonenumber = "0791234567";
        Date registrationDate = new Date();
        // Act
        address.setId(id);
        address.setFirstname(firstname);
        address.setLastname(lastname);
        address.setPhonenumber(phonenumber);
        address.setRegistrationDate(registrationDate);
        int retrievedId = address.getId();
        String retrievedFirstname = address.getFirstname();
        String retrievedLastname = address.getLastname();
        String retrievedPhonenumber = address.getPhonenumber();
        Date retrievedRegistrationDate = address.getRegistrationDate();
        // Assert
        assert retrievedId == 7;
        assert retrievedFirstname.equals("Joe");
        assert retrievedLastname.equals("Mama");
        assert retrievedPhonenumber.equals("0791234567");
        assert retrievedRegistrationDate.equals(retrievedRegistrationDate);
    }

}