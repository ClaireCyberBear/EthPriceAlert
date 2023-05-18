using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
#pragma warning disable CS8600, CS8602

class Program
{
    public class Config
    {
        public string? ApiKey { get; set; }
        public decimal PriceChangeThreshold { get; set; }
        public int CheckFrequency { get; set; }
    }
     static void Main(string[] args)
    {
        string configJson = File.ReadAllText("config.json");
        Config? config = JsonConvert.DeserializeObject<Config>(configJson);

        if (config != null)
        {
            MonitorPriceAsync(config).Wait();
        }
        else
        {
            Console.Error.WriteLine("Failed to load configuration from config.json");
        }
    }
    
    private static async Task<decimal> GetCurrentPriceAsync(string apiKey)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);
            HttpResponseMessage response = await client.GetAsync("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?id=1027");
            string responseJson = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(responseJson);

            return data.data["1027"].quote.USD.price;
        }
    }

    private static async Task MonitorPriceAsync(dynamic config)
    {
        decimal? lastPrice = null;

        while (true)
        {
            decimal currentPrice = await GetCurrentPriceAsync(config.ApiKey);
             Console.WriteLine($"Ethereum price is currently: {currentPrice}");

            if (lastPrice != null)
            {
                decimal priceChange = (currentPrice - lastPrice.Value) / lastPrice.Value * 100;

                if (Math.Abs(priceChange) >= config.PriceChangeThreshold)
                {
                    Console.WriteLine($"Ethereum price has changed by {priceChange}%.");
                }
            }

            lastPrice = currentPrice;

            await Task.Delay(TimeSpan.FromMinutes(config.CheckFrequency));
        }
    }
}