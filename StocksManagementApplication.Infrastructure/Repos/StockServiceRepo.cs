using LiveUpdates.Models;
using LiveUpdates.RepoContracts;
using Microsoft.EntityFrameworkCore;

namespace LiveUpdates.Repos
{
    public class StockServiceRepo : IStockServiceRepo
    {
        private readonly StockMarketDbContext _db;
        public StockServiceRepo(StockMarketDbContext db)
        {
            _db = db;
        }

        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            _db.Add(buyOrder);
            await _db.SaveChangesAsync();
            return buyOrder;
        }

        public async Task<List<BuyOrder>> GetBuyOrders() => await _db.BuyOrders.Select(p => p).ToListAsync();

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            _db.Add(sellOrder);
            await _db.SaveChangesAsync();

            return sellOrder;
        }

        public async Task<List<SellOrder>> GetSellOrders() => await _db.SellOrders.Select(s => s).ToListAsync();


    }
}
