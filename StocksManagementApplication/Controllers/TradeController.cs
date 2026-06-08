using FinnhubServiceInterface;
using LiveUpdates.Contracts;
using LiveUpdates.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using StocksManagementApplication.Core.DTOs;
using StocksManagementApplication.UI.Filters;


namespace LiveUpdates.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly TradingOptions _configuration;
        private readonly IFinnhubGetterService _finnhubGetterService;
        private readonly IFinnhubSearchService _finnhubSearchService;
        private readonly IConfiguration _config;
        private readonly IStocksGetterService _stocksGetterService;
        private readonly IStocksCreaterService _stocksCreaterService;
        private readonly ILogger<TradeController> _logger;
        public TradeController(IFinnhubGetterService getterService, IFinnhubSearchService searchService, IOptions<TradingOptions> configuration, IConfiguration config, IStocksGetterService stocksGetterService, IStocksCreaterService stocksCreaterService, ILogger<TradeController> logger)
        {
            _configuration = configuration.Value;
            _finnhubSearchService = searchService;
            _finnhubGetterService = getterService;
            _config = config;
            _stocksCreaterService = stocksCreaterService;
            _stocksGetterService = stocksGetterService;
            _logger = logger;
        }
        [Route("[action]")]
        [Route("/")]
        [Route("[action]/{stockSymbol?}")]
        public async Task<IActionResult> Index(string stockSymbol)
        {
            _logger.LogInformation("Index action method was accessed in TradeController");
            if (string.IsNullOrEmpty(stockSymbol))
                stockSymbol = "MSFT";

            Dictionary<string, object>? profileDictionary = await _finnhubGetterService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? quoteDictionary = await _finnhubGetterService.GetStockPriceQuote(stockSymbol);

            StockTrade stockTrade = new StockTrade() { StockName = stockSymbol, Quantity = _configuration.DefaultQuantity };

            if (profileDictionary != null && quoteDictionary != null)
            {
                stockTrade.StockSymbol = Convert.ToString(profileDictionary["ticker"]);
                stockTrade.Price = Convert.ToDouble(quoteDictionary["c"].ToString());
                stockTrade.StockName = profileDictionary["name"].ToString();
            }
            ViewBag.FinnhubToken = _config["APIKey"];
            return View(stockTrade);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            List<BuyOrderResponse> buyOrders = await _stocksGetterService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = await _stocksGetterService.GetSellOrders();

            Orders orders = new Orders() { BuyOrders = buyOrders, SellOrders = sellOrders };
            return View(orders);
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest orderRequest)
        {
            orderRequest.DateAndTimeOfOrder = DateTime.Now;
            BuyOrderResponse buyOrderResponse = await _stocksCreaterService.CreateBuyOrder(orderRequest);

            return RedirectToAction("Orders", "Trade");
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
        {
            orderRequest.DateAndTimeOfOrder = DateTime.Now;
            SellOrderResponse sellOrderResponse = await _stocksCreaterService.CreateSellOrder(orderRequest);

            return RedirectToAction("Orders", "Trade");
        }

        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            Orders orders = new Orders()
            {
                BuyOrders = await _stocksGetterService.GetBuyOrders(),
                SellOrders = await _stocksGetterService.GetSellOrders()
            };
            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Left = 20, Bottom = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
    }
}