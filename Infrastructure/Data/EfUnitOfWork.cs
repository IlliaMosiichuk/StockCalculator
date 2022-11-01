using AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly StockCalculatorContext _stockCalculatorContext;
        public EfUnitOfWork(StockCalculatorContext stockCalculatorContext)
        {
            _stockCalculatorContext = stockCalculatorContext;
        }

        public async Task<int> CommitAsync()
        {
            return await _stockCalculatorContext.SaveChangesAsync();
        }
    }
}
