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

        public async Task<List<Stock>> GetAllAsync(Helpers.QueryObject query)
        // 1. Get the query started , but dont execute it yet 

        {
            // AsQueryable() create a pending sql query , ye ef ko batata hai ki mai tumhe kuch rules dunga
            // but do not go to the db jbtak mai query banana finished na kru , if we didnt use it ef would fetch all 1000000 stocks into memory first 
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            // 2. FIltering If the user provided a CompanyName , add where clause

            if (!string.IsNullOrWhiteSpace((query.CompanyName)))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            // 3. Filtering if the user provided a Symbol , add where cluase

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            // 4. pagination calculatehow many records to skipp and how many to take 
            /*
           Skip(number)

            What it does: Tells the database to jump over the first X records. If you are on Page 2 (with 20 items per page), you need to Skip(20) to get past Page 1.Take(number)

           What it does: Tells the database to only grab X records and stop. This translates to LIMIT 20 in SQL.(Note: Skip and Take must always go at the very end of your query, right before ToListAsync()!)
            */
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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
