using System.ComponentModel.DataAnnotations;

namespace LiveUpdates.Models
{
    public class SellOrder 
    {
        public Guid SellOrderID { get; set; }

        [StringLength(20)]
        [Required]
        public string? StockSymbol { get; set; }

        [StringLength(40)]
        [Required]
        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1, 100000)]
        public double Price { get; set; }
    }
    
}
