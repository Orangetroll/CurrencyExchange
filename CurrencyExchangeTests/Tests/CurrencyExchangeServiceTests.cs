using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CurrencyExchange.Services;
using CurrencyExchange.Models;

namespace CurrencyExchange.Tests
{
    public class CurrencyExchangeServiceTests
    {
        private CurrencyExchangeService _service;

        [SetUp]
        public void Setup()
        {
            _service = new CurrencyExchangeService();
        }

        [Test]
        // Test sprawdza, czy poprawne waluty zwracają prawidłowo przekonwertowaną kwotę.
        public void Convert_ValidCurrencies_ReturnsConvertedAmount()
        {
            decimal amount = 100m;
            decimal result = _service.Convert("PLN", "USD", amount);
            Assert.AreEqual(24m, result);
        }

        [Test]
        // Test sprawdza, czy podanie niepoprawnego kodu waluty rzuca wyjątek ArgumentException.
        public void Convert_InvalidCurrency_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.Convert("PLN", "INVALID", 100m));
        }

        [Test]
        // Test sprawdza, czy podanie pustego kodu waluty rzuca wyjątek ArgumentException.
        public void Convert_EmptyCurrency_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.Convert("", "USD", 100m));
            Assert.Throws<ArgumentException>(() => _service.Convert("PLN", "", 100m));
        }

        [Test]
        // Test sprawdza, czy poprawne aktualizacje kursów walut faktycznie aktualizują stawki walut w systemie.
        public void UpdateRates_ValidCurrencies_UpdatesRates()
        {
            var newRates = new List<Currency>
            {
                new Currency { Code = "USD", Name = "US Dollar", Rate = 0.25m },
                new Currency { Code = "EUR", Name = "Euro", Rate = 0.22m }
            };

            _service.UpdateRates(newRates);
            var updatedCurrencies = _service.GetCurrencies();

            Assert.AreEqual(0.25m, updatedCurrencies.Find(c => c.Code == "USD").Rate);
            Assert.AreEqual(0.22m, updatedCurrencies.Find(c => c.Code == "EUR").Rate);
        }

        [Test]
        // Test sprawdza, czy metoda GetCurrencies zwraca pełną listę walut.
        public void GetCurrencies_ReturnsAllCurrencies()
        {
            var currencies = _service.GetCurrencies();
            Assert.AreEqual(10, currencies.Count);
        }
    }
}