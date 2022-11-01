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
    public class StockService : IStockService
    {
        private readonly IStockDailyPriceRepository _priceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockPerformanceCalculator _performanceCalculator;
        private readonly IStockApiService _apiService;
        private readonly IWeekHelper _weekHelper;

        public StockService(IStockDailyPriceRepository priceRepository,
            IUnitOfWork unitOfWork,
            IStockPerformanceCalculator performanceCalculator,
            IStockApiService apiService,
            IWeekHelper weekHelper)
        {
            _priceRepository = priceRepository;
            _unitOfWork = unitOfWork;
            _performanceCalculator = performanceCalculator;
            _apiService = apiService;
            _weekHelper = weekHelper;
        }

        public async Task<List<StockPerformanceResult>> GetLastWeekPerformance(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentException("Parameter cannot be null or empty", nameof(symbol));

            symbol = symbol.ToUpper();

            var startDate = _weekHelper.GetLastWeekStartDate();
            var endDate = _weekHelper.GetLastWeekEndDate();

            var lastWeekDailyPrices = await _priceRepository.GetPeriodPrices(symbol, startDate, endDate);
            if (!lastWeekDailyPrices.Any())
            {
                var stockPricesFromApi = await _apiService.GetWeeklyTimeSeries(symbol, startDate);

                lastWeekDailyPrices = stockPricesFromApi.Items
                    .OrderBy(p => p.Date).Select(p => new StockDailyPrice
                    {
                        Symbol = symbol,
                        ClosePrice = p.ClosePrice,
                        Date = p.Date
                    })
                    .ToList();

                _priceRepository.AddRange(lastWeekDailyPrices);
                await _unitOfWork.CommitAsync();
            }

            var lastWeekDailyPerformance = _performanceCalculator.Calculate(lastWeekDailyPrices);
            return lastWeekDailyPerformance;
        }
    }
}
