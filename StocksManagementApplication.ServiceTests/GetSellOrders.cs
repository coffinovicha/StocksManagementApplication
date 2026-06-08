using AutoFixture;
using FluentAssertions;
using LiveUpdates.Contracts;
using LiveUpdates.Models;
using LiveUpdates.RepoContracts;
using LiveUpdates.Services;
using Moq;
using StocksManagementApplication.Core.DTOs;

namespace UnitTests
{
    public class GetSellOrders
    {
        private readonly IStocksGetterService _stockGetterServices;
        private readonly IStockServiceRepo _stockServiceRepo;
        private readonly Mock<IStockServiceRepo> _stockServiceRepoMock;
        private readonly IFixture _fixture;
        public GetSellOrders()
        {
            _stockServiceRepoMock = new Mock<IStockServiceRepo>();
            _stockServiceRepo = _stockServiceRepoMock.Object;
            _stockGetterServices = new StockGetterServices(_stockServiceRepo);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetBuyOrders_ReturnsNull()
        {

            _stockServiceRepoMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(new List<SellOrder>());

            List<SellOrderResponse> sellOrderResponses = await _stockGetterServices.GetSellOrders();

            Assert.Empty(sellOrderResponses);
        }

        [Fact]
        public async Task GetBuyOrders_ProperInput()
        {
            List<SellOrder> sellOrders = _fixture.CreateMany<SellOrder>(3).ToList();
            List<SellOrderResponse> sellOrderResponses = sellOrders.Select(s => s.ToSellOrderResponse()).ToList();

            _stockServiceRepoMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(sellOrders);

            List<SellOrderResponse> sellOrderResponsesRet = await _stockGetterServices.GetSellOrders();

            sellOrderResponsesRet.Should().BeEquivalentTo(sellOrderResponses);


        }
    }
}