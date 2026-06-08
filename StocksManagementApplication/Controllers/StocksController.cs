using FinnhubServiceInterface;
using LiveUpdates.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiveUpdates.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly IFinnhubGetterService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        public StocksController(IFinnhubGetterService finnhubService, IOptions<TradingOptions> options)
        {
            _finnhubService = finnhubService;
            _tradingOptions = options;

        }

        [Route("[action]")]
        [Route("[action]/{stock?}")]
        [Route("~/[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            List<Dictionary<string, string>>? stocksDict = await _finnhubService.GetStocks();
            List<Stock> stocks = new List<Stock>();

            if (stocksDict != null)
            {
                if (!showAll && _tradingOptions.Value.Top25PopularStocks != null)
                {
                    string[]? top25Stocks = _tradingOptions!.Value.Top25PopularStocks.Split(',');
                    if (top25Stocks != null)
                    {
                        stocksDict = stocksDict.Where(s => top25Stocks.Contains(s["symbol"].ToString())).ToList();
                    }

                    stocks = stocksDict.Select(s => new Stock { StockName = s["description"].ToString(), StockSymbol = s["symbol"].ToString() }).ToList();

                }
            }
            ViewBag.Stock = stock;
            return View(stocks);
        }
    }
}