using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using Newtonsoft.Json;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class DefaultController : Controller
    {
        public async Task<IActionResult> WeatherDetail()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://the-weather-api.p.rapidapi.com/api/weather/Istanbul"),
                Headers =
    {
        { "x-rapidapi-key", "7d4d381d99mshe5918e75b67b15ap1f1272jsnf639ac9d84dc" },
        { "x-rapidapi-host", "the-weather-api.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<WeatherViewModel.Rootobject>(body);
                ViewBag.cityTemp = values.data.temp;
                return View();
            }
        }

        public async Task<IActionResult> Exchange()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("x-rapidapi-key", "7d4d381d99mshe5918e75b67b15ap1f1272jsnf639ac9d84dc");
            client.DefaultRequestHeaders.Add("x-rapidapi-host", "real-time-finance-data.p.rapidapi.com");

            var usdRequestUri = new Uri("https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol=USD&language=en&to_symbol=TRY");
            var eurRequestUri = new Uri("https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol=EUR&language=en&to_symbol=TRY");

            var usdTask = GetExchangeRateAsync(client, usdRequestUri);
            var eurTask = GetExchangeRateAsync(client, eurRequestUri);

            await Task.WhenAll(usdTask, eurTask);

            var usdValues = await usdTask;
            var eurValues = await eurTask;

            ViewBag.ExchangeUsd = usdValues.data.exchange_rate;
            ViewBag.PreviousUsd = usdValues.data.previous_close;
            ViewBag.ExchangeEur = eurValues.data.exchange_rate;
            ViewBag.PreviousEur = eurValues.data.previous_close;
            return View();
        }

        private async Task<CurrencyViewModel.Rootobject> GetExchangeRateAsync(HttpClient client, Uri requestUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CurrencyViewModel.Rootobject>(body);
            }

        }
    }
}
