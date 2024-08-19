using Application.Strategy;

namespace Infrastructure.Strategies
{
    public class USDPriceCalculationStrategy : IPriceCalculationStrategy
    {
        public decimal CalculatePrice(decimal basePrice)
        {
            return basePrice * 0.92m;
        }

        public string GetCurrency()
        {
            return "USD";
        }
    }
}
