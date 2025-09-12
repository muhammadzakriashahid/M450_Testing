package ch.tbz.m450.util;

import ch.tbz.m450.repository.Address;

import java.util.Comparator;

public class AddressComparator implements Comparator<Address> {

    @Override
    public int compare(Address a1, Address a2) {
        int lastNameComparison = a1.getLastname().compareTo(a2.getLastname());
        if (lastNameComparison != 0) {
            return lastNameComparison;
        }
        
        int firstNameComparison = a1.getFirstname().compareTo(a2.getFirstname());
        if (firstNameComparison != 0) {
            return firstNameComparison;
        }
        
        int phoneComparison = a1.getPhonenumber().compareTo(a2.getPhonenumber());
        if (phoneComparison != 0) {
            return phoneComparison;
        }
        
        return a1.getRegistrationDate().compareTo(a2.getRegistrationDate());
    }

}
