package ch.schule.bank.junit5;

import ch.schule.Account;
import ch.schule.SalaryAccount;
import ch.schule.SavingsAccount;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

/**
 * Tests für die Klasse Account.
 */
public class AccountTests {
    /**
     * Tested die Initialisierung eines Kontos.
     */
    @Test
    public void testInit() {
        String testId = "ACC123";
        Account account = new Account(testId);

        assertEquals(testId, account.getId(), "Account ID should be set correctly");
        assertEquals(0, account.getBalance(), "Initial balance should be zero");
    }

    /**
     * Testet das Einzahlen auf ein Konto.
     */
    @Test
    public void testDeposit() {
        Account account = new Account("ACC1");
        boolean result = account.deposit(20250101, 1000);
        assertTrue(result, "Deposit should succeed with positive amount");
        assertEquals(1000, account.getBalance(), "Balance should reflect deposited amount");

        // Negative deposit
        result = account.deposit(20250102, -500);
        assertFalse(result, "Deposit should fail with negative amount");
        assertEquals(1000, account.getBalance(), "Balance should not change after failed deposit");
    }

    /**
     * Testet das Abheben von einem Konto.
     */
    @Test
    public void testWithdraw() {
        Account account = new Account("ACC2");
        account.deposit(20250101, 2000);
        boolean result = account.withdraw(20250102, 500);
        assertTrue(result, "Withdraw should succeed with positive amount");
        assertEquals(1500, account.getBalance(), "Balance should decrease after withdrawal");

        // Negative withdraw
        result = account.withdraw(20250103, -100);
        assertFalse(result, "Withdraw should fail with negative amount");
        assertEquals(1500, account.getBalance(), "Balance should not change after failed withdrawal");
    }

    /**
     * Tests the reference from SavingsAccount.
     */
    @Test
    public void testReferences() {
        SavingsAccount sa = new SavingsAccount("SAV1");
        assertTrue(sa instanceof Account, "SavingsAccount should be an Account");
        SalaryAccount sla = new SalaryAccount("SAL1", 0L);
        assertTrue(sla instanceof Account, "SalaryAccount should be an Account");
    }

    /**
     * Teste the canTransact Flag.
     */
    @Test
    public void testCanTransact() {
        Account account = new Account("ACC3");
        // No bookings yet, should be able to transact
        assertTrue(account.canTransact(20250101), "Should be able to transact if no bookings");
        account.deposit(20250101, 1000);
        // Same date as last booking
        assertTrue(account.canTransact(20250101), "Should be able to transact on same date as last booking");
        // Later date
        assertTrue(account.canTransact(20250102), "Should be able to transact on later date");
        // Earlier date
        assertFalse(account.canTransact(20241231), "Should not be able to transact on earlier date");
    }

    /**
     * Experimente mit print().
     */
    @Test
    public void testPrint() {
        Account account = new Account("ACC4");
        account.deposit(20250101, 1000);
        account.withdraw(20250102, 200);
        assertDoesNotThrow(() -> account.print(), "Print should not throw an exception");
    }

    /**
     * Experimente mit print(year, month).
     */
    @Test
    public void testMonthlyPrint() {
        Account account = new Account("ACC5");
        account.deposit(20250101, 1000);
        account.withdraw(20250115, 300);
        assertDoesNotThrow(() -> account.print(2025, 1), "Monthly print should not throw an exception");
    }
}