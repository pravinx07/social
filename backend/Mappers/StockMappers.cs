using backend.Dtos.Stock;
using backend.Models;

namespace backend.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Price = stockModel.Price,
                LastDividend = stockModel.LastDividend,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList(),
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Price = (decimal)stockDto.Price, // Cast or use .Value since it has [Required] attribute
                LastDividend = stockDto.LastDividend,
                Industry = stockDto.Industry ?? string.Empty,
                MarketCap = stockDto.MarketCap ?? string.Empty,


            };

        }
    }


}