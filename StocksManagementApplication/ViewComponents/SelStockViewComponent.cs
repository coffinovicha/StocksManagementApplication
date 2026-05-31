using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksManagementApplication.Core.ServiceContracts;

namespace LiveUpdates.ViewComponents
{

    public class SelStockViewComponent : ViewComponent
    {
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stockService;
        private readonly IConfiguration _configuration;

        public SelStockViewComponent(IOptions<TradingOptions> options, IFinnhubService finnhubService, IStocksService stocksService, IConfiguration configuration)
        {
            _configuration = configuration;
            _stockService = stocksService;
            _finnhubService = finnhubService;
            _tradingOptions = options;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            Dictionary<string, object>? info = null;
            if (stockSymbol is not null)
            {
                info = await _finnhubService.GetCompanyProfile(stockSymbol);
                var quote = await _finnhubService.GetStockPriceQuote(stockSymbol);

                if (info != null && quote != null)
                {
                    info.Add("price", quote["c"]);
                }
            }
            if (info != null && info.ContainsKey("logo")) return View(info);
            return Content("");

        }

    }
}
