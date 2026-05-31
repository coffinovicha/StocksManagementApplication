using AutoFixture;
using FluentAssertions;
using LiveUpdates.Models;
using Moq;
using StocksManagementApplication.Core.Domain.RepoContracts;
using StocksManagementApplication.Core.DTOs;
using StocksManagementApplication.Core.ServiceContracts;
using StocksManagementApplication.Core.Services;


namespace UnitTests
{
    public class CreateSellOrder
    {
        private readonly IStocksService _stockService;
        private readonly IStockServiceRepo _stockServiceRepo;
        private readonly Mock<IStockServiceRepo> _stockServiceRepoMock;
        private readonly IFixture _fixture;

        public CreateSellOrder()
        {
            _fixture = new Fixture();
            _stockServiceRepoMock = new Mock<IStockServiceRepo>();
            _stockServiceRepo = _stockServiceRepoMock.Object;
            _stockService = new StockServices(_stockServiceRepo);
        }

        [Fact]
        public async Task CreateSellOrder_CreateSellOrderNull()
        {
           SellOrderRequest? SellOrder = null;

           Func<Task> action = async () =>
           {
                await _stockService.CreateSellOrder(SellOrder);
           };

           await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateSellOrder_CreateSellOrderZero()
        {
           SellOrderRequest? SellOrder = new SellOrderRequest() { Quantity = 0 };

            Func<Task> action = async () =>
            {
               await _stockService.CreateSellOrder(SellOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_CreateSellOrder100001()
        {
            SellOrderRequest? SellOrder = new SellOrderRequest() { Quantity = 100001 };

            Func<Task> action = async () =>
            {
               await _stockService.CreateSellOrder(SellOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_CreateSellOrderPriceZero()
        {
            SellOrderRequest? SellOrder = new SellOrderRequest() { Price = 0 };

            Func<Task> action = async () =>
            {
               await _stockService.CreateSellOrder(SellOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_CreateSellOrderPrice10001()
        {
            SellOrderRequest? SellOrder = new SellOrderRequest() { Price = 10001 };

            Func<Task> action = async () =>
            {
                await _stockService.CreateSellOrder(SellOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_CreateSellOrderSymbolNull()
        {
            SellOrderRequest? SellOrder = new SellOrderRequest() { StockSymbol = null };

            Func<Task> action = async () =>
            {
               await _stockService.CreateSellOrder(SellOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_CreateSellOrderStockDateTimeCheck()
        {
            SellOrderRequest SellOrder = new SellOrderRequest() { DateAndTimeOfOrder = new DateTime(1999, 12, 31) };

            Func<Task> action = async () =>
            {
                await _stockService.CreateSellOrder(SellOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateSellOrder_ProperInput()
        {
            SellOrderRequest sellOrderRequest = _fixture.Create<SellOrderRequest>();
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockServiceRepoMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            SellOrderResponse sellOrderResponse = await _stockService.CreateSellOrder(sellOrderRequest);
            sellOrder.SellOrderID = sellOrderResponse.SellOrderID;

            sellOrderResponse.SellOrderID.Should().NotBeEmpty();
            sellOrderResponse.Should().Be(sellOrder.ToSellOrderResponse());
        }
    }
}
