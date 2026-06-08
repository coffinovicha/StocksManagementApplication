using StocksManagementApplication.Core.DTOs;

namespace LiveUpdates.Contracts
{
    public interface IStocksGetterService
    {
        Task<List<BuyOrderResponse>> GetBuyOrders();
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
