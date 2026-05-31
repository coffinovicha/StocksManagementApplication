using LiveUpdates.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using StocksManagementApplication.Core.DTOs;
using StocksManagementApplication.Core.ServiceContracts;


namespace LiveUpdates.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly TradingOptions _configuration;
        private readonly IFinnhubService _finnhubService;
        private readonly IConfiguration _config;
        private readonly IStocksService _stocksService;
        public TradeController(IFinnhubService service, IOptions<TradingOptions> configuration, IConfiguration config, IStocksService stocksService)
        {
            _configuration = configuration.Value;
            _finnhubService = service;
            _config = config;
            _stocksService = stocksService;
        }
        [Route("[action]")]
        [Route("/")]
        [Route("[action]/{stockSymbol?}")]
        public async Task<IActionResult> Index(string stockSymbol)
        {
            if (string.IsNullOrEmpty(stockSymbol))
                stockSymbol = "MSFT";

            Dictionary<string, object>? profileDictionary = await _finnhubService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? quoteDictionary = await _finnhubService.GetStockPriceQuote(stockSymbol);

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
            List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = await _stocksService.GetSellOrders();

            Orders orders = new Orders() { BuyOrders = buyOrders, SellOrders = sellOrders };
            return View(orders);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(er => er.Errors).Select(er => er.ErrorMessage).ToList();
                return RedirectToAction("Index", "Trade");
            }
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction("Orders", "Trade");
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(er => er.Errors).Select(er => er.ErrorMessage).ToList();
                return RedirectToAction("Index", "Trade");
            }
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            return RedirectToAction("Orders", "Trade");
        }

        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            Orders orders = new Orders() 
            { 
                BuyOrders = await _stocksService.GetBuyOrders(), 
                SellOrders = await _stocksService.GetSellOrders() 
            };
            return new ViewAsPdf("OrdersPDF", orders, ViewData) 
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Left = 20, Bottom = 20 },
                PageOrientation =  Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
    }
}
