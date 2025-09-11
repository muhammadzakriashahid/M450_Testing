package ch.schule.bank.junit5;

import ch.schule.Booking;
import org.junit.jupiter.api.Test;

import java.awt.print.Book;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.fail;


/**
 * Tests für die Klasse Booking.
 *
 * @author Luigi Cavuoti
 * @version 1.1
 */
public class BookingTests
{
	private Booking booking;
	/**
	 * Tests f�r die Erzeugung von Buchungen.
	 */
	@Test
	public void testInitialization()
	{
		// Arrange
		int date = 248;
		long amount = 1000;
		// Act
		Booking booking = new Booking(date, amount);
		// Assert
		assertEquals(248, booking.getDate());
		assertEquals(1000, booking.getAmount());
	}

	/**
	 * Experimente mit print().
	 */
	@Test
	public void testPrint()
	{
		// Arrange
		int date = 248;
		long amount = 1000;
		long balance = 5000;

		// Capture System.out
		// tutorial -> https://www.geeksforgeeks.org/advance-java/unit-testing-of-system-out-println-with-junit/
		ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));

		// Act
		Booking booking = new Booking(date, amount);
		booking.print(balance);

		// Assert
		String expected = ch.schule.BankUtils.formatBankDate(date)
                + " " + ch.schule.BankUtils.formatAmount(amount)
                + " " + ch.schule.BankUtils.formatAmount(balance + amount)
                + System.lineSeparator();
		assertEquals(expected, outContent.toString());
	}
}
