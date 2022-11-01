using AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StockCalculatorContext : DbContext
    {
        public StockCalculatorContext(DbContextOptions<StockCalculatorContext> options)
          : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        public DbSet<StockDailyPrice> StockDailyPrices { get; set; }
    }
}
