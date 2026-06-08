using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksManagementApplication.Core.DTOs
{
    public class IOrderRequest
    {
        public required string StockSymbol { get; set; }
        public required string StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
    }
}
