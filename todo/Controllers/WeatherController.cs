using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using todo.Models;

namespace todo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            string apiKey = "39f8ecaf506c4f76b3f55139222906";
            string apiUrl = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={city}&days=3&aqi=yes&alerts=yes";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                WeatherData weatherData = await response.Content.ReadFromJsonAsync<WeatherData>();

                decimal temperature = weatherData.Current.temp_c;
                string condition = weatherData.Current.condition.text;

                string message = $"The weather in {city} is: temp {temperature} condition {condition}";

                return Ok(message);
            }

            return NotFound();
        }

        // Inner class WeatherData
        private class WeatherData
        {
            public CurrentData Current { get; set; }
        }

        private class CurrentData
        {
            public decimal temp_c { get; set; }
            public ConditionData condition { get; set; }
        }

        private class ConditionData
        {
            public string text { get; set; }
        }
    }
}
