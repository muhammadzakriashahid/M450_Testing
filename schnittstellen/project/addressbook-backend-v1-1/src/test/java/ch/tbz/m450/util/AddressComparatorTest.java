package ch.tbz.m450.util;

import ch.tbz.m450.repository.Address;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

class AddressComparatorTest {

    @Test
    void testComparator() {
        Address a1 = new Address(1, "John", "Doe", "123", new Date());
        Address a2 = new Address(2, "Jane", "Doe", "456", new Date());
        Address a3 = new Address(3, "John", "Smith", "789", new Date());

        List<Address> addresses = new ArrayList<>(List.of(a3, a1, a2));
        addresses.sort(new AddressComparator());

        // correct order: Doe (Jane), Doe (John), Smith (John)
        assertEquals("Jane", addresses.get(0).getFirstname());
        assertEquals("John", addresses.get(1).getFirstname());
        assertEquals("John", addresses.get(2).getFirstname());
    }
}