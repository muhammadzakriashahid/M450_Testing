package test;

import main.Calculator;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class CalculatorTest {
    private final Calculator calculator = new Calculator();

    @Test
    void testAdd() {
        // Arrange
        double a = 5.0;
        double b = 3.0;
        // Act
        double result = calculator.add(a, b);
        // Assert
        assertEquals(8, result);
    }

    @Test
    void testSubtraction() {
        // Arrange
        double a = 5.0;
        double b = 3.0;
        // Act
        double result = calculator.subtract(a, b);
        // Assert
        assertEquals(2, result);
    }

    @Test
    void testMultiplication() {
        // Arrange
        double a = 5.0;
        double b = 3.0;
        // Act
        double result = calculator.multiply(a, b);
        // Assert
        assertEquals(15, result);
    }

    @Test
    void testDivision() {
        // Arrange
        double a = 15.0;
        double b = 3.0;
        double c = 0;
        // Act
        double result1 = calculator.divide(a, b);
        // Assert
        assertEquals(5, result1);
        assertThrows(IllegalArgumentException.class, () -> calculator.divide(b, c));
    }

}