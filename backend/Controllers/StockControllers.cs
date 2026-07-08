using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.Stock;
using backend.Interface;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{

    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {


        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            // _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockToUpdate = new backend.Models.Stock
            {
                Symbol = updateDto.Symbol,
                CompanyName = updateDto.CompanyName,
                Price = updateDto.Price,
                Industry = updateDto.Industry,
                LastDividend = updateDto.LastDividend,
                MarketCap = updateDto.MarketCap,
            };

            var updatedStock = await _stockRepo.UpdateAsync(id, stockToUpdate);

            if (updatedStock == null)
            {
                return NotFound();
            }

            // stockModel.Symbol = updateDto.Symbol;


            // await _context.SaveChangesAsync();
            return Ok(updatedStock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)

        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            // _context.Stocks.Remove(stockModel);
            // await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}