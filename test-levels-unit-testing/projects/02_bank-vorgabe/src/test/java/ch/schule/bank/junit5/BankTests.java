package ch.schule.bank.junit5;

import ch.schule.Bank;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;


/**
 * Tests f�r die Klasse 'Bank'.
 *
 * @author xxxx
 * @version 1.0
 */
public class BankTests {

    /**
     * Tests to create new Accounts.
     */
    @Test
    public void testCreate() {
        Bank bank = new Bank();
        String savingsAccountId = bank.createSavingsAccount();
        String promoYouthAccountId = bank.createPromoYouthSavingsAccount();
        String salaryAccountId = bank.createSalaryAccount(-5000);

        assertNotNull(savingsAccountId);
        assertNotNull(promoYouthAccountId);
        assertNotNull(salaryAccountId);
    }

    /**
     * Tests depositing into an account.
     */
    @Test
    public void testDeposit() {
        Bank bank = new Bank();
        String accountId = bank.createSavingsAccount();

        boolean success = bank.deposit(accountId, 20231010, 1000);

        assertTrue(success);
        assertEquals(1000, bank.getBalance(accountId));
    }

    /**
     * Tests withdrawing from an account.
     */
    @Test
    public void testWithdraw() {
        Bank bank = new Bank();
        String accountId = bank.createSavingsAccount();
        bank.deposit(accountId, 20231010, 1000);

        boolean success = bank.withdraw(accountId, 20231011, 500);

        assertTrue(success);
        assertEquals(500, bank.getBalance(accountId));
    }

    /**
     * Experiments with print().
     */
    @Test
    public void testPrint() {
        Bank bank = new Bank();
        String accountId = bank.createSavingsAccount();
        bank.deposit(accountId, 20231010, 1000);

        // This test assumes the print method outputs to the console.
        // You can manually verify the output or use a library like System Rules to capture console output.
        bank.print(accountId);
    }

    /**
     * Experiments with print(year, month).
     */
    @Test
    public void testMonthlyPrint() {
        Bank bank = new Bank();
        String accountId = bank.createSavingsAccount();
        bank.deposit(accountId, 20231010, 1000);

        // This test assumes the print method outputs to the console.
        bank.print(accountId, 2023, 10);
    }

    /**
     * Tests the total balance of the bank.
     */
    @Test
    public void testBalance() {
        Bank bank = new Bank();
        String accountId1 = bank.createSavingsAccount();
        String accountId2 = bank.createSavingsAccount();
        bank.deposit(accountId1, 20231010, 1000);
        bank.deposit(accountId2, 20231010, 2000);

        assertEquals(3000, -bank.getBalance());
    }

    /**
     * Tests the output of the "top 5" accounts.
     */
    @Test
    public void testTop5() {
        Bank bank = new Bank();
        for (int i = 0; i < 10; i++) {
            String accountId = bank.createSavingsAccount();
            bank.deposit(accountId, 20231010, i * 1000);
        }

        // This test assumes the printTop5 method outputs to the console.
        bank.printTop5();
    }

    /**
     * Tests the output of the "bottom 5" accounts.
     */
    @Test
    public void testBottom5() {
        Bank bank = new Bank();
        for (int i = 0; i < 10; i++) {
            String accountId = bank.createSavingsAccount();
            bank.deposit(accountId, 20231010, i * 1000);
        }

        // This test assumes the printBottom5 method outputs to the console.
        bank.printBottom5();
    }
}
