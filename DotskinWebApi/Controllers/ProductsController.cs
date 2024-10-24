using DotskinWebApi.Data;
using DotskinWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // DELETE: products/delete/1
        [HttpDelete("delete/{id}")]
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

        // POST: products/add
        [HttpPost("add")]
        public async Task<ActionResult<Product>> Add([FromBody] Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // POST: products/add2?id=1&name=piim&price=4.5&isactive=false
        [HttpPost("add2")]
        public async Task<ActionResult<Product>> Add2([FromQuery] int id, [FromQuery] string name, [FromQuery] double price, [FromQuery] bool isActive)
        {
            var product = new Product(id, name, price, isActive);
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
                product.Price *= course;
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
        [HttpPut("update")]
        public async Task<ActionResult<Product>> Update([FromBody] Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);

            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.IsActive = product.IsActive;

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
            var product = await _context.Products.OrderByDescending(p => p.Price).FirstOrDefaultAsync();
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
