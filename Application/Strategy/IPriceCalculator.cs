namespace Application.Strategy
{
    public interface IPriceCalculator
    {
        decimal Calculate(decimal basePrice);
    }
}
