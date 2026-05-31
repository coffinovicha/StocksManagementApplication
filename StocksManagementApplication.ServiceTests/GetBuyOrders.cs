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
    public class GetBuyOrders
    {
        private readonly IStocksService _stockServices;
        private readonly IStockServiceRepo _stockServiceRepo;
        private readonly Mock<IStockServiceRepo> _stockServiceRepoMock;
        private readonly IFixture _fixture;

        public GetBuyOrders()
        {
            _fixture = new Fixture();
            _stockServiceRepoMock = new Mock<IStockServiceRepo>();
            _stockServiceRepo = _stockServiceRepoMock.Object;
            _stockServices = new StockServices(_stockServiceRepo);
        }

        [Fact]
        public async Task GetBuyOrders_ReturnsNull()
        {
            _stockServiceRepoMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(new List<BuyOrder>());

            List<BuyOrderResponse> buyOrderResponses = await _stockServices.GetBuyOrders();

            buyOrderResponses.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBuyOrders_ProperInput()
        {

            List<BuyOrder> buyOrders = _fixture.CreateMany<BuyOrder>(3).ToList();
            List<BuyOrderResponse> buyOrderResponses = buyOrders.Select(b => b.ToBuyOrderResponse()).ToList();

            _stockServiceRepoMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrders);

            List<BuyOrderResponse> buyOrderResponsesRet = await _stockServices.GetBuyOrders();

            buyOrderResponsesRet.Should().BeEquivalentTo(buyOrders);
        }
    }
}
