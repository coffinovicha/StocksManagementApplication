namespace FinnhubServiceInterface
{
    public interface IFinnhubSearchService
    {
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}
