import static org.junit.Assert.assertEquals;

import org.junit.Test;

public class PreisberechnungTests {
    @Test
    public void testHello() {
        double baseprice = 30000;
        double specialprice = 2000;
        double extraprice = 1000;
        int extras = 5;
        double discount = 5;

        double expected = 3400;
        double actual = App.calculatePrice(baseprice, specialprice, extraprice, extras, discount);

        assertEquals(expected, actual, 0.01);
    }
}
