using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public interface IStockDailyPriceRepository : IRepository<StockDailyPrice>
    {
        Task<IEnumerable<StockDailyPrice>> GetPeriodPrices(string symbol, DateTime startDate, DateTime endDate);
    }
}
