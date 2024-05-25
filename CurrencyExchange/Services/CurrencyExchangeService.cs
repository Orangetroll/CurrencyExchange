using CurrencyExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyExchange.Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private List<Currency> _currencies;

        public CurrencyExchangeService()
        {
            _currencies = new List<Currency>
            {
                new Currency { Code = "PLN", Name = "Polish Zloty", Rate = 1m },
                new Currency { Code = "USD", Name = "US Dollar", Rate = 0.24m },
                new Currency { Code = "EUR", Name = "Euro", Rate = 0.21m },
                new Currency { Code = "GBP", Name = "British Pound", Rate = 0.18m },
                new Currency { Code = "JPY", Name = "Japanese Yen", Rate = 26m },
                new Currency { Code = "AUD", Name = "Australian Dollar", Rate = 0.31m },
                new Currency { Code = "CAD", Name = "Canadian Dollar", Rate = 0.29m },
                new Currency { Code = "CHF", Name = "Swiss Franc", Rate = 0.23m },
                new Currency { Code = "CNY", Name = "Chinese Yuan", Rate = 1.53m },
                new Currency { Code = "INR", Name = "Indian Rupee", Rate = 18.3m }
            };
        }

        public decimal Convert(string fromCurrency, string toCurrency, decimal amount)
        {
            if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
                throw new ArgumentException("Currency codes cannot be null or empty");

            var from = _currencies.FirstOrDefault(c => c.Code == fromCurrency.ToUpper());
            var to = _currencies.FirstOrDefault(c => c.Code == toCurrency.ToUpper());

            if (from == null || to == null)
                throw new ArgumentException("Invalid currency code");

            var convertedAmount = amount * (to.Rate / from.Rate);
            return Math.Round(convertedAmount, 2);
        }

        public void UpdateRates(IEnumerable<Currency> currencies)
        {
            foreach (var currency in currencies)
            {
                var existingCurrency = _currencies.FirstOrDefault(c => c.Code == currency.Code);
                if (existingCurrency != null)
                {
                    existingCurrency.Rate = currency.Rate;
                }
                else
                {
                    _currencies.Add(currency);
                }
            }
        }

        public List<Currency> GetCurrencies()
        {
            return _currencies;
        }
    }
}
