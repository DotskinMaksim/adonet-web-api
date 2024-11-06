using DotskinWebApi.Data;
using DotskinWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DotskinWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get([FromQuery] string category = null)
        {
            // Если категория не указана, то возвращаем все продукты
            var products = string.IsNullOrEmpty(category)
                ? await _context.Products.ToListAsync()  // Все продукты
                : await _context.Products
                    .Where(p => p.Category == category)  // Фильтруем по категории
                    .ToListAsync();

            return Ok(products);
        }

        // GET: products/withtax
        [HttpGet("withtax")]
        public async Task<ActionResult<IEnumerable<Product>>> GetWithTax([FromQuery] string category = null)
        {
            // Если категория не указана, то возвращаем все продукты
            var products = string.IsNullOrEmpty(category)
                ? await _context.Products.ToListAsync()  // Все продукты
                : await _context.Products
                    .Where(p => p.Category == category)  // Фильтруем по категории
                    .ToListAsync();

            // Применяем налог и округляем цену
            foreach (var item in products)
            {
                item.PricePerUnit *= 1.20;  // Применяем налог
                item.PricePerUnit = Math.Round(item.PricePerUnit, 2);  // Округляем до 2 знаков после запятой
            }

            return Ok(products);
        }




        // DELETE: products/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok("Product deleted.");
        }

        // POST: products
        [HttpPost]
        public async Task<ActionResult<Product>> Add([FromQuery] int id, [FromQuery] string name, [FromQuery] double pricePerUnit, [FromQuery] bool isActive,
             [FromQuery] string unit, [FromQuery] bool hasBottle, [FromQuery] string imageUrl, [FromQuery] int amountInStock, [FromQuery] string category)
        {
            var product = new Product(id, name, pricePerUnit, isActive, unit, hasBottle, imageUrl, amountInStock, category);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // PATCH: products/price-in-dollars/1.5
        [HttpPatch("price-in-dollars/{course}")]
        public async Task<ActionResult<IEnumerable<Product>>> InDollars(double course)
        {
            var products = await _context.Products.ToListAsync();

            foreach (var product in products)
            {
                product.PricePerUnit *= course;
            }

            await _context.SaveChangesAsync();
            return Ok(products);
        }

        // GET: products/delete-all
        [HttpDelete("delete-all")]
        public async Task<ActionResult> DeleteAll()
        {
            var products = await _context.Products.ToListAsync();

            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();

            return Ok("All products deleted.");
        }

        // PUT: products/update
        [HttpPut]
        public async Task<ActionResult<Product>> Update([FromBody] Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);

            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.PricePerUnit = product.PricePerUnit;
            existingProduct.IsActive = product.IsActive;
            existingProduct.Unit = product.Unit;
            existingProduct.AmountInStock = product.AmountInStock;
            existingProduct.HasBottle = product.HasBottle;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.Category = product.Category;

            await _context.SaveChangesAsync();
            return Ok(existingProduct);
        }

        // GET: product/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }

        // GET: products/most-expensive
        [HttpGet("most-expensive")]
        public async Task<ActionResult<Product>> GetTheMostExpensiveProduct()
        {
            var product = await _context.Products.OrderByDescending(p => p.PricePerUnit).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound("No products found.");
            }

            return Ok(product);
        }

        // GET: products/all-activity-to-false
        [HttpGet("all-activity-to-false")]
        public async Task<ActionResult<IEnumerable<Product>>> ChangeAllActivityToFalse()
        {
            var products = await _context.Products.ToListAsync();

            foreach (var product in products)
            {
                product.IsActive = false;
            }

            await _context.SaveChangesAsync();
            return Ok(products);
        }
    }
}
