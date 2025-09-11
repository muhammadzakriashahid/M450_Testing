package ch.schule.bank.junit5;

import ch.schule.SavingsAccount;



/**
 * Tests f�r die Klasse SavingsAccount.
 *
 * @author Roger H. J&ouml;rg
 * @version 1.0
 */

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;


/**
 * Tests für die Klasse SavingsAccount.
 *
 * @author XXX
 * @version 1.0
 */
public class SavingsAccountTests
{
	/**
	 * Tests creating a SavingsAccount and verifying initial state.
	 */
	@Test
	public void testCreateSavingsAccount() {
		SavingsAccount account = new SavingsAccount("SAVINGS1");

		assertNotNull(account, "Account should be created successfully");
		assertEquals(0, account.getBalance(), "Initial balance should be 0");
		assertEquals("SAVINGS1", account.getId(), "Account ID should match");
	}
}

