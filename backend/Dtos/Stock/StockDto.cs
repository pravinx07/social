namespace backend.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }

        public decimal LastDividend { get; set; }
        public string Industry { get; set; }
        public string MarketCap { get; set; }

    }
}