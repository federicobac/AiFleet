public interface ICurrencyExchangeRateProvider
{
    decimal GetExchangeRate(string currencyCode);
}