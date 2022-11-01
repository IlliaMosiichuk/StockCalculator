using AppCore.Entities;
using AppCore.Interfaces;
using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class StockPerformanceCalculator : IStockPerformanceCalculator
    {
        public List<StockPerformanceResult> Calculate(IEnumerable<StockDailyPrice> dailyPrices)
        {
            if (dailyPrices == null || !dailyPrices.Any())
                throw new ArgumentException("Input cannot be null or empty.", nameof(dailyPrices));

            var firstDayPrice = dailyPrices.First();
            var result = new List<StockPerformanceResult>()
            {
                new StockPerformanceResult
                {
                    Date = firstDayPrice.Date,
                    Performance = 0
                }
            };

            foreach (var price in dailyPrices.Skip(1))
            {
                result.Add(new StockPerformanceResult
                {
                    Date = price.Date,
                    Performance = ((price.ClosePrice - firstDayPrice.ClosePrice) / firstDayPrice.ClosePrice)
                });
            }

            return result;
        }
    }
}
