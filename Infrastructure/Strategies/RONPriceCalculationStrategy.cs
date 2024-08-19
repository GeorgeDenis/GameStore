using Application.Strategy;

namespace Infrastructure.Strategies
{
    public class RONPriceCalculationStrategy : IPriceCalculationStrategy
    {
        public decimal CalculatePrice(decimal basePrice)
        {
            return basePrice * 4.98m;
        }

        public string GetCurrency()
        {
            return "RON";
        }
    }
}
