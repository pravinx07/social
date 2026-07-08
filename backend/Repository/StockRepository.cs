using backend.Data;
using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;



namespace backend.Repository
{
    public class StockRepository : IStockRepository

    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Stock> CreateAsync(Stock stocktModel)
        {
            await _context.Stocks.AddAsync(stocktModel);
            await _context.SaveChangesAsync();
            return stocktModel;
        }

        public async Task<Stock?> UpdateAsync(int id, Stock stockModel)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(c => c.Id == id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stockModel.Symbol;
            existingStock.CompanyName = stockModel.CompanyName;
            existingStock.Price = stockModel.Price;
            existingStock.Change = stockModel.Change;
            existingStock.ChangePercent = stockModel.ChangePercent;
            existingStock.LastDividend = stockModel.LastDividend;
            existingStock.Industry = stockModel.Industry;
            existingStock.MarketCap = stockModel.MarketCap;
            await _context.SaveChangesAsync();
            return existingStock;
        }
        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(c => c.Id == id);
            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
return stockModel;
        }
    }
}
