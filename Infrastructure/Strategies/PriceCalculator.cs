using Application.Strategy;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Infrastructure.Strategies
{
    public class PriceCalculator : IPriceCalculator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnumerable<IPriceCalculationStrategy> _strategyStrategies;

        public PriceCalculator(IHttpContextAccessor httpContextAccessor, IEnumerable<IPriceCalculationStrategy> strategyStrategies)
        {
            _httpContextAccessor = httpContextAccessor;
            _strategyStrategies = strategyStrategies;
        }

        public decimal Calculate(decimal basePrice)
        {
            var currency = _httpContextAccessor.HttpContext.Request.Headers["X-Currency"].ToString();
            var strategy = _strategyStrategies.FirstOrDefault(x => x.GetCurrency() == currency);

            if (string.IsNullOrEmpty(currency) || strategy == null)
            {
                return basePrice;
            }

            return strategy.CalculatePrice(basePrice);
        }
    }
}
