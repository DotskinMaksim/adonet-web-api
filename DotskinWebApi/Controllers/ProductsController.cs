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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(
            [FromQuery] string category = null,
            [FromQuery] int offset = 0,
            [FromQuery] int? limit = null,
            [FromQuery] bool withTax = false,
            [FromQuery] bool withBottlePrice = false
        )
        {
            if (limit.HasValue && limit <= 0)
            {
                return BadRequest("Limit must be greater than 0.");
            }

            var query = _context.Products
                .Where(p => p.AmountInStock > 0);

            // Filter by category if provided
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category.NameEn == category);  // Make sure to use the correct field for filtering
            }

            // Apply pagination
            if (limit.HasValue)
            {
                query = query.Skip(offset).Take(limit.Value);
            }

            var products = await query.ToListAsync();

            // Apply tax if needed
            if (withTax)
            {
                foreach (var item in products)
                {
                    item.PricePerUnit = TaxCalculator.GetWithTax(item.PricePerUnit);
                    item.PricePerUnit = Math.Round(item.PricePerUnit, 2);
                }
            }

            // Apply bottle price if needed
            if (withBottlePrice)
            {
                foreach (var item in products)
                {
                    if (item.HasBottle)
                    {
                        item.PricePerUnit += 0.10;
                    }
                }
            }

            return Ok(products);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories([FromQuery] string lang = "en")
        {
            var categories = await _context.Categories.ToListAsync();

            if (categories == null || categories.Count == 0)
            {
                return NotFound("No categories found");
            }

            // Возвращаем категории с учетом выбранного языка
            foreach (var category in categories)
            {
                category.Name = lang switch
                {
                    "et" => category.NameEt,
                    "ru" => category.NameRu,
                    _ => category.NameEn // по умолчанию на английском
                };
            }

            return Ok(categories);
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
        public async Task<ActionResult<Product>> Add(
            [FromQuery] string name, 
            [FromQuery] double pricePerUnit, 
            [FromQuery] bool isActive,
            [FromQuery] string unit, 
            [FromQuery] bool hasBottle, 
            [FromQuery] string imageUrl, 
            [FromQuery] double amountInStock, 
            [FromQuery] int categoryId)
        {       

            var product = new Product(name, pricePerUnit, isActive, unit, hasBottle, imageUrl, amountInStock, categoryId);

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
