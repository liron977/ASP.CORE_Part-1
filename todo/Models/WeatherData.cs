using System.Collections.Generic;

namespace todo.Models
{
    public class WeatherData
    {
        public Forecast Forecast { get; set; }
    }

    public class Forecast
    {
        public List<ForecastDay> ForecastDay { get; set; }
    }

    public class ForecastDay
    {
        public string Date { get; set; }
        public Day Day { get; set; }
    }

    public class Day
    {
        public double MaxTempC { get; set; }
        public double MinTempC { get; set; }
        // Add other properties as needed
    }
}
