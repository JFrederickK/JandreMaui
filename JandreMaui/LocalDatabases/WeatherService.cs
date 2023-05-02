using JandreMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JandreMaui.LocalDatabases
{
    public class WeatherService
    {
       HttpClient client;

        public WeatherService() { 
        client = new HttpClient();
        }
        public async Task<WeatherData> GetWeatherDataAsync(string query)
        {
            WeatherData weatherData = null;
            try
            {
                var respone = await client.GetAsync(query);
                if (respone.IsSuccessStatusCode) {
                var content = await respone.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception exs)
            {
                Debug.WriteLine(exs.Message);
                throw;
            }
            return weatherData;
        }
    }
}
