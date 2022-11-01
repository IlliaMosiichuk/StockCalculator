using AppCore.Entities;
using AppCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly StockCalculatorContext _dbContext;

        protected DbSet<T> _dbSet
        {
            get
            {
                return _dbContext.Set<T>();
            }
        }

        public EfRepository(StockCalculatorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbSet.ToListAsync();
            }

            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
