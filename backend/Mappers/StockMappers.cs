using backend.Models;
using backend.Dtos.Stock;

namespace backend.Mappers{
 public static class StockMappers{
    public static StockDto ToStockDto(this Stock stockModel){
        return new StockDto{
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Price = stockModel.Price,
            LastDividend = stockModel.LastDividend,
            MarketCap = stockModel.MarketCap,
        };
    }

    public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
    {
       return new Stock 
       {
        Symbol = stockDto.Symbol,
        CompanyName = stockDto.CompanyName,
        Price = stockDto.Price,
        LastDividend = stockDto.LastDividend,
        Industry = stockDto.Industry,
        MarketCap = stockDto.MarketCap,
       };
    }
 }


}