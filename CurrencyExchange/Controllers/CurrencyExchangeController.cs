using CurrencyExchange.Models;
using CurrencyExchange.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyExchange.Controllers
{
    public class CurrencyExchangeController : Controller
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public CurrencyExchangeController(ICurrencyExchangeService currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Currencies = _currencyExchangeService.GetCurrencies();
            return View();
        }

        [HttpPost]
        public IActionResult Convert(string fromCurrency, string toCurrency, decimal amount)
        {
            ViewBag.Currencies = _currencyExchangeService.GetCurrencies();
            try
            {
                var result = _currencyExchangeService.Convert(fromCurrency, toCurrency, amount);
                ViewData["Result"] = result;
                ViewData["ResultCurrency"] = toCurrency.ToUpper();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult UpdateRates(List<Currency> currencies)
        {
            _currencyExchangeService.UpdateRates(currencies);
            return RedirectToAction("Index");
        }
    }
}
