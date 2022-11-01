using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AlphaVantageStockApiService : IStockApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;  

        public AlphaVantageStockApiService(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<DailyTimeSeriesResponse> GetWeeklyTimeSeries(string symbol, DateTime startDate)
        {
            var apiKey = _configuration["AlphaVantageApiKey"];
            var httpClient = _httpClientFactory.CreateClient("AlphaVantage");
            var url = $"query?function=TIME_SERIES_DAILY&symbol={symbol}&outputsize=compact&apikey={apiKey}";
            
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            // use of dynamic because of weird response format, e.g. properties are called "1. open", "2. high"
            var responseJson = await response.Content.ReadAsStringAsync();
            var dynamicResponse = JsonConvert.DeserializeObject<dynamic>(responseJson);

            //AlphaVantage always returns 200 OK, even in case of any errors,
            //so we need to check if ErrorMessage or Note property (API limit) exists
            var errorMessage = Convert.ToString(dynamicResponse["Error Message"]);
            if (!string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException($"AlphaVantage API error: {errorMessage}");
            
            var note = Convert.ToString(dynamicResponse["Note"]);
            if (!string.IsNullOrEmpty(note))
                throw new InvalidOperationException($"AlphaVantage API limit reached.");

            var prices = dynamicResponse["Time Series (Daily)"];
            var result = new DailyTimeSeriesResponse();
            for (var days = 0; days < 5; days++)
            {
                var date = startDate.AddDays(days);
                var dateProp = $"{date.Year}-{date.Month}-{date.Day}";
                var closePrice = prices[dateProp]["4. close"];
                result.Items.Add(new DailyTimeSeriesResponseItem
                {
                    Date = date,
                    ClosePrice = decimal.Parse(Convert.ToString(closePrice), CultureInfo.InvariantCulture)
                });
            }

            return result;
        }
    }
}
