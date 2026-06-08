using LiveUpdates.Contracts;
using LiveUpdates.RepoContracts;
using StocksManagementApplication.Core.DTOs;

namespace LiveUpdates.Services
{
    public class StockGetterServices : IStocksGetterService
    {
        private readonly IStockServiceRepo _stockServiceRepo;
       
        public StockGetterServices(IStockServiceRepo stockServiceRepo)
        {
            _stockServiceRepo = stockServiceRepo;
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders() => (await _stockServiceRepo.GetBuyOrders()).Select(b => b.ToBuyOrderResponse()).ToList();

        public async Task<List<SellOrderResponse>> GetSellOrders() => (await _stockServiceRepo.GetSellOrders()).Select(s => s.ToSellOrderResponse()).ToList();
    }
}
