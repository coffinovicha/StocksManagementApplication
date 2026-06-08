namespace FinnhubServiceInterface
{
    public interface IFinnhubGetterService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string Symbol);
        Task<Dictionary<string, object>?> GetStockPriceQuote(string Symbol);
        Task<List<Dictionary<string, string>>?> GetStocks();

    }
}
