namespace Application.Strategy
{
    public interface IPriceCalculationStrategy
    {
        decimal CalculatePrice(decimal basePrice);
        string GetCurrency();
    }
}
