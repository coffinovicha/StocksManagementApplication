namespace LiveUpdates.Models
{
    public class Stock
    {
        public string? StockName { get; set; }
        public string? StockSymbol { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Stock) return false;
            return StockName == ((Stock)obj).StockName && StockSymbol == ((Stock)obj).StockSymbol;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
