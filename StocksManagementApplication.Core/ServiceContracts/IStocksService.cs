using StocksManagementApplication.Core.DTOs;

namespace StocksManagementApplication.Core.ServiceContracts
{
    public interface IStocksService
    {
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);
        Task<List<BuyOrderResponse>> GetBuyOrders();
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
