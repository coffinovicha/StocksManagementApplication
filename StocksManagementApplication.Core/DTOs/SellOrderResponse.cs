using LiveUpdates.Models;

namespace StocksManagementApplication.Core.DTOs
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(SellOrderResponse)) return false;
            return (SellOrderID == ((SellOrderResponse)obj).SellOrderID && StockName == ((SellOrderResponse)obj).StockName && StockSymbol == ((SellOrderResponse)obj).StockSymbol && DateAndTimeOfOrder == ((SellOrderResponse)obj).DateAndTimeOfOrder && Quantity == ((SellOrderResponse)obj).Quantity && Price == ((SellOrderResponse)obj).Price && TradeAmount == ((SellOrderResponse)obj).TradeAmount);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse() { SellOrderID = sellOrder.SellOrderID, DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder, Price = sellOrder.Price, Quantity = sellOrder.Quantity, StockName = sellOrder.StockName, StockSymbol = sellOrder.StockSymbol, TradeAmount = sellOrder.Quantity * sellOrder.Price };
        }
    }
}
