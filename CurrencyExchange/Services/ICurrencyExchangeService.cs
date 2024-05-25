using CurrencyExchange.Models;

namespace CurrencyExchange.Services
{
    public interface ICurrencyExchangeService
    {
        decimal Convert(string fromCurrency, string toCurrency, decimal amount);
        void UpdateRates(IEnumerable<Currency> currencies);
        List<Currency> GetCurrencies(); 
    }
}
