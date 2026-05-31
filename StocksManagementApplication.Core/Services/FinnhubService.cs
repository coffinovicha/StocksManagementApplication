using StocksManagementApplication.Core.Domain.RepoContracts;
using StocksManagementApplication.Core.ServiceContracts;
using System.Text.Json;

namespace StocksManagementApplication.Core.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IFinnhubServiceRepo _finnhubRepo;
        public FinnhubService(IFinnhubServiceRepo finnhubServiceRepo)
        {
            _finnhubRepo = finnhubServiceRepo;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol) => await _finnhubRepo.GetCompanyProfile(stockSymbol);


        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol) => await _finnhubRepo.GetStockPriceQuote(stockSymbol);

        public async Task<List<Dictionary<string, string>>?> GetStocks() => await _finnhubRepo.GetStocks();
        
        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch) => await _finnhubRepo.SearchStocks(stockSymbolToSearch);
        
    }
}
