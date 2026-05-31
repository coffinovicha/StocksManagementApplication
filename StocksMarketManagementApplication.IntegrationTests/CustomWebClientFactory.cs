using LiveUpdates.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace UnitTests
{
    public class CustomWebClientFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services => {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(IOptionsFactory<StockMarketDbContext>));
                if (descriptor != null) services.Remove(descriptor);
                services.AddDbContext<StockMarketDbContext>(options => {
                    options.UseInMemoryDatabase("Testing");
                });
            });
        }
    }
}
