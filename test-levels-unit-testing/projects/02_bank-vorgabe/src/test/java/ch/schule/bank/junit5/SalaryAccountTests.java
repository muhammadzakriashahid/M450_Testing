package ch.schule.bank.junit5;

import ch.schule.SalaryAccount;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;


/**
 * Tests der Klasse SalaryAccount.
 *
 * @author XXX
 * @version 1.1
 */
public class SalaryAccountTests
{

	/**
	 * Tests creating a SalaryAccount and verifying initial state.
	 */
	@Test
	public void testCreateSalaryAccount() {
		SalaryAccount account = new SalaryAccount("SALARY1", -5000);

		assertNotNull(account, "Account should be created successfully");
		assertEquals(0, account.getBalance(), "Initial balance should be 0");
		assertEquals("SALARY1", account.getId(), "Account ID should match");
	}
}
