using LiveUpdates.Contracts;
using LiveUpdates.ValidationHelpers;
using LiveUpdates.Models;
using LiveUpdates.RepoContracts;
using StocksManagementApplication.Core.DTOs;

namespace LiveUpdates.Services
{
    public class StockCreaterServices : IStocksCreaterService
    {
        private readonly IStockServiceRepo _stockServiceRepo;
       
        public StockCreaterServices(IStockServiceRepo stockServiceRepo)
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
    }
}
