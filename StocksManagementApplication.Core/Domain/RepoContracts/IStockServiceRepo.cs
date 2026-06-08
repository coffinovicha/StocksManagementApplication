using LiveUpdates.Models;
namespace LiveUpdates.RepoContracts
{
    public interface IStockServiceRepo
    {
        Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

        Task<List<BuyOrder>> GetBuyOrders();

        Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

        Task<List<SellOrder>> GetSellOrders();

    }
}
