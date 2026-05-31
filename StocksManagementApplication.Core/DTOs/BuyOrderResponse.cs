using LiveUpdates.Models;

namespace StocksManagementApplication.Core.DTOs
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(BuyOrderResponse)) return false;
            return (BuyOrderID == ((BuyOrderResponse)obj).BuyOrderID && StockName == ((BuyOrderResponse)obj).StockName && StockSymbol == ((BuyOrderResponse)obj).StockSymbol && DateAndTimeOfOrder == ((BuyOrderResponse)obj).DateAndTimeOfOrder && Quantity == ((BuyOrderResponse)obj).Quantity && Price == ((BuyOrderResponse)obj).Price && TradeAmount == ((BuyOrderResponse)obj).TradeAmount);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class BuyOrderExtensions 
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder) 
        {
            return new BuyOrderResponse() { BuyOrderID = buyOrder.BuyOrderID, DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder, Price = buyOrder.Price, Quantity = buyOrder.Quantity, StockName = buyOrder.StockName, StockSymbol = buyOrder.StockSymbol, TradeAmount = buyOrder.Price * buyOrder.Quantity };
        }
    }
}
