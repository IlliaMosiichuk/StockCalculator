using AppCore.Entities;
using AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StockDailyPriceRepository : EfRepository<StockDailyPrice>, IStockDailyPriceRepository
    {
        public StockDailyPriceRepository(StockCalculatorContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<StockDailyPrice>> GetPeriodPrices(string symbol, DateTime startDate, DateTime endDate)
        {
            return (await GetAll(p => p.Symbol == symbol
                && p.Date >= startDate && p.Date <= endDate))
                .OrderBy(p => p.Date);
        }
    }
}
