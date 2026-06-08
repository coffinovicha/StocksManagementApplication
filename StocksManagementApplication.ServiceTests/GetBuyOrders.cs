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
    public class GetBuyOrders
    {
        private readonly IStocksGetterService _stockGetterServices;
        private readonly IStockServiceRepo _stockServiceRepo;
        private readonly Mock<IStockServiceRepo> _stockServiceRepoMock;
        private readonly IFixture _fixture;

        public GetBuyOrders()
        {
            _fixture = new Fixture();
            _stockServiceRepoMock = new Mock<IStockServiceRepo>();
            _stockServiceRepo = _stockServiceRepoMock.Object;
            _stockGetterServices = new StockGetterServices(_stockServiceRepo);
        }

        [Fact]
        public async Task GetBuyOrders_ReturnsNull()
        {
            _stockServiceRepoMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(new List<BuyOrder>());

            List<BuyOrderResponse> buyOrderResponses = await _stockGetterServices.GetBuyOrders();

            buyOrderResponses.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBuyOrders_ProperInput()
        {

            List<BuyOrder> buyOrders = _fixture.CreateMany<BuyOrder>(3).ToList();
            List<BuyOrderResponse> buyOrderResponses = buyOrders.Select(b => b.ToBuyOrderResponse()).ToList();

            _stockServiceRepoMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrders);

            List<BuyOrderResponse> buyOrderResponsesRet = await _stockGetterServices.GetBuyOrders();

            buyOrderResponsesRet.Should().BeEquivalentTo(buyOrders);
        }
    }
}