using FluentAssertions;
using LiveUpdates.Contracts;
using LiveUpdates.Models;
using LiveUpdates.RepoContracts;
using LiveUpdates.Services;
using Moq;
using StocksManagementApplication.Core.DTOs;
using System;

namespace UnitTests
{
    public class CreateBuyOrder
    {
        private readonly IStocksCreaterService _stockCreaterService;
        private readonly Mock<IStockServiceRepo> _stockServiceRepoMock;
        private readonly IStockServiceRepo _stockServiceRepo;

        public CreateBuyOrder()
        {
            _stockServiceRepoMock = new Mock<IStockServiceRepo>();
            _stockServiceRepo = _stockServiceRepoMock.Object;
            _stockCreaterService = new StockCreaterServices(_stockServiceRepo);
        }

        #region CreateBuyOrder
        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestNull()
        {
            BuyOrderRequest? buyOrder = null;

            Func<Task> action = async () =>
            {
                await _stockCreaterService.CreateBuyOrder(buyOrder);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestQuantityZero()
        {
            BuyOrderRequest? buyOrder = new BuyOrderRequest() { Quantity = 0 };

            Func<Task> action = async () =>
            {
                await _stockCreaterService.CreateBuyOrder(buyOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestQuantity100001()
        {
            BuyOrderRequest? buyOrder = new BuyOrderRequest() { Quantity = 100001 };

            Func<Task> action = async () =>
            {
                await _stockCreaterService.CreateBuyOrder(buyOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestPriceZero()
        {
            BuyOrderRequest? buyOrder = new BuyOrderRequest() { Price = 0 };

            Func<Task> action = async () =>
            {
                await _stockCreaterService.CreateBuyOrder(buyOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestPrice10001()
        {
            BuyOrderRequest? buyOrder = new BuyOrderRequest() { Price = 10001 };

            Func<Task> action = async () =>
            {
                await _stockCreaterService.CreateBuyOrder(buyOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestStockSymbolNull()
        {
            BuyOrderRequest? buyOrder = new BuyOrderRequest() { StockSymbol = null };

            Func<Task> action = async () =>
            {
                await _stockCreaterService.CreateBuyOrder(buyOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestStockDateTimeCheck()
        {
            BuyOrderRequest buyOrder = new BuyOrderRequest() { DateAndTimeOfOrder = new DateTime(1999, 12, 31) };

            Func<Task> action = async () =>
            {
                await _stockCreaterService.CreateBuyOrder(buyOrder);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateBuyOrder_ProperInput()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", DateAndTimeOfOrder = new DateTime(2001, 01, 01), Price = 100, StockName = "Name", Quantity = 100 };
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockServiceRepoMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            BuyOrderResponse buyOrderResponse = await _stockCreaterService.CreateBuyOrder(buyOrderRequest);
            buyOrder.BuyOrderID = buyOrderResponse.BuyOrderID;

            buyOrderResponse.BuyOrderID.Should().NotBeEmpty();
            buyOrderResponse.Should().Be(buyOrder.ToBuyOrderResponse());

        }
        #endregion
    }
}