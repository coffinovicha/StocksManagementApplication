using LiveUpdates.Models;

namespace StocksManagementApplication.Core.Domain.RepoContracts
{
    public interface IStockServiceRepo
    {
        Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

        Task<List<BuyOrder>> GetBuyOrders();

        Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

        Task<List<SellOrder>> GetSellOrders();

    }
}
