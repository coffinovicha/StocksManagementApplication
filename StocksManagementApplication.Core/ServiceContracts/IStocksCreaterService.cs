
using StocksManagementApplication.Core.DTOs;

namespace LiveUpdates.Contracts
{
    public interface IStocksCreaterService
    {
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);
    }
}
