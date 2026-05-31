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
    public class GetSellOrders
    {
        private readonly IStocksService _stockServices;
        private readonly IStockServiceRepo _stockServiceRepo;
        private readonly Mock<IStockServiceRepo> _stockServiceRepoMock;
        private readonly IFixture _fixture;
        public GetSellOrders()
        {
            _stockServiceRepoMock = new Mock<IStockServiceRepo>();
            _stockServiceRepo = _stockServiceRepoMock.Object;
            _stockServices = new StockServices(_stockServiceRepo);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetBuyOrders_ReturnsNull()
        {

            _stockServiceRepoMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(new List<SellOrder>());

            List<SellOrderResponse> sellOrderResponses = await _stockServices.GetSellOrders();

            Assert.Empty(sellOrderResponses);
        }

        [Fact]
        public async Task GetBuyOrders_ProperInput()
        {
            List<SellOrder> sellOrders = _fixture.CreateMany<SellOrder>(3).ToList();
            List<SellOrderResponse> sellOrderResponses = sellOrders.Select(s => s.ToSellOrderResponse()).ToList();

            _stockServiceRepoMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(sellOrders);

            List<SellOrderResponse> sellOrderResponsesRet = await _stockServices.GetSellOrders();

            sellOrderResponsesRet.Should().BeEquivalentTo(sellOrderResponses);

            
        }
    }
}
