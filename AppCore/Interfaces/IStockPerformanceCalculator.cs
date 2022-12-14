using AppCore.Entities;
using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public interface IStockPerformanceCalculator
    {
        List<StockPerformanceResult> Calculate(IEnumerable<StockDailyPrice> dailyPrices);
    }
}
