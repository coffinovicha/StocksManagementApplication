using FinnhubServiceInterface;
using LiveUpdates.Contracts;
using LiveUpdates.Models;
using LiveUpdates.RepoContracts;
using LiveUpdates.Repos;
using LiveUpdates.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Service;
using StocksManagementApplication.Core.Domain.RepoContracts;
using StocksManagementApplication.UI.Middleware;

namespace LiveUpdates
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider serviceProvider, LoggerConfiguration config) => {
                config.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(serviceProvider);
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IFinnhubGetterService, FinnhubGetterService>();
            builder.Services.AddScoped<IFinnhubSearchService, FinnhubSearchService>();
            builder.Services.AddScoped<IFinnhubServiceRepo, FinnhubServiceRepo>();
            builder.Services.AddScoped<IStocksGetterService, StockGetterServices>();
            builder.Services.AddScoped<IStocksCreaterService, StockCreaterServices>();
            builder.Services.AddScoped<IStockServiceRepo, StockServiceRepo>();
            builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
            builder.Services.AddDbContext<StockMarketDbContext>(options =>
            {
                options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddHttpLogging(options => {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });
            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseExceptionHandlingMiddleware();
            }

            if (!builder.Environment.IsEnvironment("Testing"))
                Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");


            app.UseSerilogRequestLogging();
            app.UseHttpLogging();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}

public partial class Program { };

