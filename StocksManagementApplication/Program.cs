using LiveUpdates.Models;
using LiveUpdates.Repos;
using Microsoft.EntityFrameworkCore;
using StocksManagementApplication.Core.Domain.RepoContracts;
using StocksManagementApplication.Core.ServiceContracts;
using StocksManagementApplication.Core.Services;

namespace LiveUpdates
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IFinnhubService, FinnhubService>();
            builder.Services.AddScoped<IFinnhubServiceRepo, FinnhubServiceRepo>();
            builder.Services.AddScoped<IStocksService, StockServices>();
            builder.Services.AddScoped<IStockServiceRepo, StockServiceRepo>();
            builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
            builder.Services.AddDbContext<StockMarketDbContext>(options => 
            {
                options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var app = builder.Build();

            if(!builder.Environment.IsEnvironment("Testing"))
                Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}

public partial class Program { };

