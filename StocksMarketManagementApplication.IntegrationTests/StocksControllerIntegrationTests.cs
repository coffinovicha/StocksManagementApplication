using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using FluentAssertions;
using Fizzler.Systems.HtmlAgilityPack;

namespace UnitTests
{
    public class StocksControllerIntegrationTests : IClassFixture<CustomWebClientFactory>
    {
        private readonly HttpClient _httpClient;

        public StocksControllerIntegrationTests(CustomWebClientFactory customWebClientFactory)
        {
            _httpClient = customWebClientFactory.CreateClient();
        }

        [Fact]
        public async Task Explore_ReturnsProperViewWithDataInsideIt() 
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("Trade/Index/MSFT");
            string body = await httpResponseMessage.Content.ReadAsStringAsync();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(body);
            HtmlNode htmlNode = document.DocumentNode;

            httpResponseMessage.IsSuccessStatusCode.Should().BeTrue();
            htmlNode.QuerySelectorAll("span.price").Should().NotBeNull();
        }
    }
}
