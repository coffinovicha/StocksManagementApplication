using Microsoft.Extensions.Configuration;
using StocksManagementApplication.Core.Domain.RepoContracts;
using System.Text.Json;


namespace LiveUpdates.Repos
{
    public class FinnhubServiceRepo : IFinnhubServiceRepo
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FinnhubServiceRepo(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["APIKey"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = await response.Content.ReadAsStreamAsync();

                StreamReader streamReader = new StreamReader(stream);

                string answer = await streamReader.ReadToEndAsync();

                Dictionary<string, object>? dictFromFinnhub = JsonSerializer.Deserialize<Dictionary<string, object>>(answer);

                if (dictFromFinnhub == null) throw new InvalidOperationException("Data was not fetched");
                if (dictFromFinnhub.ContainsKey("error")) throw new InvalidOperationException(dictFromFinnhub["error"].ToString());
                return dictFromFinnhub;

            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($" https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["APIKey"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = await httpResponse.Content.ReadAsStreamAsync();
                StreamReader streamReader = new StreamReader(stream);

                string response = await streamReader.ReadToEndAsync();

                Dictionary<string, object>? answer = JsonSerializer.Deserialize<Dictionary<string, object>>(response);


                if (answer == null) throw new InvalidOperationException("Data was not fetched");
                if (answer.ContainsKey("error")) throw new InvalidOperationException(answer["error"].ToString());
                return answer;
            }
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks() 
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient()) 
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["APIKey"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequestMessage);

                string stream = await httpResponse.Content.ReadAsStringAsync();

                List<Dictionary<string, string>>? answer = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(stream);

                if (answer == null)
                    throw new InvalidOperationException("No response from server");

                return answer;
            }
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch) 
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient()) 
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["APIKey"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequestMessage);

                string response = await httpResponse.Content.ReadAsStringAsync();

                Dictionary<string, object>? answer = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (answer == null) throw new InvalidOperationException("Data was not fetched");
                if (answer.ContainsKey("error")) throw new InvalidOperationException(answer["error"].ToString());

                return answer;
            }
        }
    }
}
