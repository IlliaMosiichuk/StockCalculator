using AppCore.Entities;
using AppCore.Extensions;
using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ILogger<StockController> _logger;


        public StockController(
            IStockService stockService,
            ILogger<StockController> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        [HttpGet("{symbol}/lastWeekPerformance")]
        public async Task<IActionResult> GetLastWeekPerformance(string symbol)
        {
            try
            {
                var weeklySymbolPerformance = await _stockService.GetLastWeekPerformance(symbol);
                var weeklySpyPerformance = await _stockService.GetLastWeekPerformance("SPY");
                var response = new GetPerformanceResponse
                {
                    Items = weeklySymbolPerformance.Select(symbolPerformance => new GetPerformanceResponseItem
                    {
                        Date = symbolPerformance.Date.ToString("d", CultureInfo.InvariantCulture),
                        SymbolPerformance = symbolPerformance.Performance.ToPercents(),
                        SpyPerformance = weeklySpyPerformance[weeklySymbolPerformance.IndexOf(symbolPerformance)].Performance.ToPercents()
                    })
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong.", ex);
                return BadRequest(ex.Message);
            }

        }

    }
}
