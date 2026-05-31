using LiveUpdates.ValidationHelpers;
using LiveUpdates.Models;
using StocksManagementApplication.Core.ServiceContracts;
using StocksManagementApplication.Core.DTOs;
using StocksManagementApplication.Core.Domain.RepoContracts;

namespace StocksManagementApplication.Core.Services
{
    public class StockServices : IStocksService
    {
        private readonly IStockServiceRepo _stockServiceRepo;

        public StockServices(IStockServiceRepo stockServiceRepo)
        {
            _stockServiceRepo = stockServiceRepo;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            BuyOrder buyOrderFromRepo = await _stockServiceRepo.CreateBuyOrder(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            SellOrder sellOrderFromRepo = await _stockServiceRepo.CreateSellOrder(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders() => (await _stockServiceRepo.GetBuyOrders()).Select(b => b.ToBuyOrderResponse()).ToList();


        public async Task<List<SellOrderResponse>> GetSellOrders() => (await _stockServiceRepo.GetSellOrders()).Select(s => s.ToSellOrderResponse()).ToList();
    }
}
