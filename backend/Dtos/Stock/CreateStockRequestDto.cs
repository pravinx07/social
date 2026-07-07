using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }
        
        [Required]
        [Range(0, 1000000)]
        public decimal? Price { get; set; } // Using nullable decimal + Required
        
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }

        public decimal LastDividend { get; set; }
        
        [Required]
        public string? Industry { get; set; }
        
        [Required]
        public string? MarketCap { get; set; }

    }
}

// data validation is only for input dtos .