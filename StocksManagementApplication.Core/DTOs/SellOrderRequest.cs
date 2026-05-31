using System.ComponentModel.DataAnnotations;
using LiveUpdates.Models;
using StocksApp.CustomValidations;

namespace StocksManagementApplication.Core.DTOs
{
    public class SellOrderRequest
    {
        [Required]
        public string? StockSymbol { get; set; }
        [Required]
        public string? StockName { get; set; }
        [MinDateValidation("01, 01, 2000")]
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000)]
        public uint Quantity { get; set; }
        [Range(1, 100000)]
        public double Price { get; set; }

        public SellOrder ToSellOrder()
        {
            return new SellOrder() { StockSymbol = StockSymbol, StockName = StockName, DateAndTimeOfOrder = DateAndTimeOfOrder, Quantity = Quantity, Price = Price };
        }
    }
}
