using LiveUpdates.Models;
using StocksApp.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace StocksManagementApplication.Core.DTOs
{
    public class BuyOrderRequest
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

        public BuyOrder ToBuyOrder() 
        {
            return new BuyOrder() { StockSymbol = StockSymbol, StockName = StockName, DateAndTimeOfOrder = DateAndTimeOfOrder, Quantity = Quantity, Price = Price };
        }
    }
}
