using FinnhubServiceInterface;
using Microsoft.Extensions.Logging;
using StocksManagementApplication.Core.Domain.RepoContracts;

namespace Service
{
    public class FinnhubGetterService : IFinnhubGetterService
    {
        private readonly IFinnhubServiceRepo _finnhubRepo;
        private readonly ILogger<FinnhubGetterService> _logger;

        public FinnhubGetterService(IFinnhubServiceRepo finnhubServiceRepo, ILogger<FinnhubGetterService> logger)
        {
            _finnhubRepo = finnhubServiceRepo;
            _logger = logger;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            _logger.LogInformation("GetCompanyProfile was accessed in FinnhubService");
            return await _finnhubRepo.GetCompanyProfile(stockSymbol);
        }


        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            _logger.LogInformation("GetStockPriceQuote was accessed in FinnhubService");
            return await _finnhubRepo.GetStockPriceQuote(stockSymbol);
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks() => await _finnhubRepo.GetStocks();
        
        
    }
}
