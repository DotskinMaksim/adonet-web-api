namespace DotskinWebApi.Models
{
    public class TaxCalculator
    {
        private const double DefaultTaxRate = 1.20;
        public static double GetWithTax(double itemPrice, double taxRate = DefaultTaxRate)
        {
            return itemPrice * taxRate;
        }
    }
}
