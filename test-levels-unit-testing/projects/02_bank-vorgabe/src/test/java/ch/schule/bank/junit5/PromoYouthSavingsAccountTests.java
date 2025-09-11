package ch.schule.bank.junit5;

import ch.schule.PromoYouthSavingsAccount;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

/**
 * Tests für das Promo-Jugend-Sparkonto.
 *
 * @author XXXX
 * @version 1.0
 */
public class PromoYouthSavingsAccountTests
{
	/**
	 * Tests depositing a valid amount with the 1% bonus.
	 */
	@Test
	public void testDepositWithBonus() {
		PromoYouthSavingsAccount account = new PromoYouthSavingsAccount("YOUTH1");

		boolean success = account.deposit(20250101, 1000);

		assertTrue(success, "Deposit should succeed");
		assertEquals(1010, account.getBalance(), "Balance should include 1% bonus");
	}
}
