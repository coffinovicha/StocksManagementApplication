using AutoFixture;
using FinnhubServiceInterface;
using FluentAssertions;
using LiveUpdates;
using LiveUpdates.Controllers;
using LiveUpdates.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;


namespace UnitTests
{
    public class StocksControllerTests
    {
        private readonly IFinnhubGetterService _finnhubService;
        private readonly Mock<IFinnhubGetterService> _finnhubServiceMock;
        private readonly IOptions<TradingOptions> _options;
        private readonly IFixture _fixture;
        public StocksControllerTests()
        {
            _fixture = new Fixture();
            _options = Options.Create(new TradingOptions
            {
                DefaultQuantity = 100,
                Top25PopularStocks = "AAPL,MSFT,AMZN"
            });
            _finnhubServiceMock = new Mock<IFinnhubGetterService>();
            _finnhubService = _finnhubServiceMock.Object;
        }

        [Fact]
        public async Task StocksController_ExploreReturnsExploreViewWithListIfNullSupplied()
        {
            StocksController stocksController = new StocksController(_finnhubService, _options);

            List<Dictionary<string, string>>? stocksDict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(@"[{'currency':'USD','description':'APPLE INC','displaySymbol':'AAPL','figi':'BBG000B9XRY4','isin':null,'mic':'XNAS','shareClassFIGI':'BBG001S5N8V8','symbol':'AAPL','symbol2':'','type':'Common Stock'}, {'currency':'USD','description':'MICROSOFT CORP','displaySymbol':'MSFT','figi':'BBG000BPH459','isin':null,'mic':'XNAS','shareClassFIGI':'BBG001S5TD05','symbol':'MSFT','symbol2':'','type':'Common Stock'}, {'currency':'USD','description':'AMAZON.COM INC','displaySymbol':'AMZN','figi':'BBG000BVPV84','isin':null,'mic':'XNAS','shareClassFIGI':'BBG001S5PQL7','symbol':'AMZN','symbol2':'','type':'Common Stock'}]");

            _finnhubServiceMock.Setup(temp => temp.GetStocks()).ReturnsAsync(stocksDict);

            List<Stock> expected = stocksDict!.Select(temp => new Stock() { StockName = temp["description"].ToString(), StockSymbol = temp["symbol"].ToString() }).ToList();

            IActionResult actionResult = await stocksController.Explore(null);

            ViewResult viewResult = actionResult.Should().BeOfType<ViewResult>().Subject;
            viewResult.ViewData.Model.Should().BeAssignableTo<List<Stock>>();
            viewResult.ViewData.Model.Should().BeEquivalentTo(expected);

        }
    }
}