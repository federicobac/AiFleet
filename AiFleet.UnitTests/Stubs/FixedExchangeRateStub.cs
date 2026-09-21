public class FixedExchangeRateStub : ICurrencyExchangeRateProvider
{
    public Dictionary<string, decimal> Rates { get; set; } = new();

    public decimal GetExchangeRate(string currencyCode)
    {
        return Rates.TryGetValue(currencyCode, out var rate) ? rate : 1.00m;
    }
}