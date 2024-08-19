using Application.Strategy;

namespace Infrastructure.Strategies
{
    public class EURPriceCalculationStrategy : IPriceCalculationStrategy
    {
        public decimal CalculatePrice(decimal basePrice)
        {
            return basePrice;
        }

        public string GetCurrency()
        {
            return "EUR";
        }
    }
}
