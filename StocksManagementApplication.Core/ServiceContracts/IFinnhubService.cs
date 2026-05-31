namespace StocksManagementApplication.Core.ServiceContracts
{
    public interface IFinnhubService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string Symbol);
        Task<Dictionary<string, object>?> GetStockPriceQuote(string Symbol);
        Task<List<Dictionary<string, string>>?> GetStocks();
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);

    }
}
