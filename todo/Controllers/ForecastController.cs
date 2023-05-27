using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using todo.Models;

namespace todo.Controllers
{
    [ApiController]
    [Route("api/forecast")]
    public class ForecastController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ForecastController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetForecast(string q)
        {
            string apiKey = "39f8ecaf506c4f76b3f55139222906";
            string apiUrl = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={q}&days=3&aqi=yes&alerts=yes";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var weatherData = await response.Content.ReadFromJsonAsync<WeatherData>();

                List<ForecastDay> forecast = weatherData?.Forecast?.ForecastDay;

                if (forecast != null)
                {
                    return Ok(forecast);
                }
            }

            return NotFound();
        }
    }
}
