using FinnhubServiceInterface;
using StocksManagementApplication.Core.Domain.RepoContracts;


namespace Service
{
    public class FinnhubSearchService : IFinnhubSearchService
    {
        private readonly IFinnhubServiceRepo _finnhubRepo;

        public FinnhubSearchService(IFinnhubServiceRepo finnhubServiceRepo)
        {
            _finnhubRepo = finnhubServiceRepo;
        }
        
        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch) => await _finnhubRepo.SearchStocks(stockSymbolToSearch);
        
    }
}
