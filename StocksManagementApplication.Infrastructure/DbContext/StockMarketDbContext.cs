using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StocksManagementApplication.Core.Domain.IdentityEntities;

namespace LiveUpdates.Models
{
    public class StockMarketDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public StockMarketDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<BuyOrder> BuyOrders { get; set; }
        public DbSet<SellOrder> SellOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
        }

        public List<BuyOrder> GetBuyOrders() 
        {
            return BuyOrders.FromSqlRaw("EXECUTE [dbo].[GetBuyOrders]").ToList();
        }

        public List<SellOrder> GetSellOrders()
        {
            return SellOrders.FromSqlRaw("EXECUTE [dbo].[GetSellOrders]").ToList();
        }

        public int sp_InsertBuyOrder(BuyOrder buyOrder) 
        {
            SqlParameter[] sqlParameters = { 
                new SqlParameter("@BuyOrderID", buyOrder.BuyOrderID),
                new SqlParameter("@StockSymbol", buyOrder.StockSymbol),
                new SqlParameter("@StockName", buyOrder.StockName),
                new SqlParameter("@DateAndTimeOfOrder", buyOrder.DateAndTimeOfOrder),
                new SqlParameter("@Quantity", buyOrder.Quantity),
                new SqlParameter("@Price", buyOrder.Price)
            };

            return Database.ExecuteSqlRaw("[dbo].[InsertBuyOrder] @BuyOrderID, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price", sqlParameters);
        }

        public int sp_InsertSellOrder(SellOrder sellOrder)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@SekkOrderID", sellOrder.SellOrderID),
                new SqlParameter("@StockSymbol", sellOrder.StockSymbol),
                new SqlParameter("@StockName", sellOrder.StockName),
                new SqlParameter("@DateAndTimeOfOrder", sellOrder.DateAndTimeOfOrder),
                new SqlParameter("@Quantity", sellOrder.Quantity),
                new SqlParameter("@Price", sellOrder.Price)
            };

            return Database.ExecuteSqlRaw("[dbo].[InsertSellOrder] @SellOrderID, @StockSymbol, @StockName, @DateAndTimeOfOrder, @Quantity, @Price", sqlParameters);
        }
    }
}
