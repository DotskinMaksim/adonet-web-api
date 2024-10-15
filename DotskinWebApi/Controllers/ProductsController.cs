using DotskinWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotskinWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new()
        {
        new Product(1,"Koola", 1.5, true),
        new Product(2,"Fanta", 1.0, false),
        new Product(3,"Sprite", 1.7, true),
        new Product(4,"Vichy", 2.0, true),
        new Product(5,"Vitamin well", 2.5, true)
        };


        // GET: products
        [HttpGet]
        public List<Product> Get()
        {
            return _products;
        }

        // DELETE: products/delete/0
        [HttpDelete("delete/{index}")]
        public List<Product> Delete(int index)
        {
            _products.RemoveAt(index-1);
            return _products;
        }

        // DELETE: products/delete2/0
        [HttpDelete("delete2/{index}")]
        public string Delete2(int index)
        {
            _products.RemoveAt(index-1);
            return "Kustutatud!";
        }

        // POST: products/add/6/testtoode/5.0/true
        [HttpPost("add")]
        public List<Product> Add([FromBody] Product product)
        {
            _products.Add(product);
            return _products;
        }

        // POST products/add?id=1&name=piim&price=4.5&isactive=false
        [HttpPost("add2")] 
        public List<Product> Add2([FromQuery] int id, [FromQuery] string name, [FromQuery] double price, [FromQuery] bool isActive)
        {
            Product product = new Product(id, name, price, isActive);
            _products.Add(product);
            return _products;
        }


        // PATCH products/price-in-dollars/1.5
        [HttpPatch("price-in-dollars/{course}")]
        public List<Product> InDollars(double course)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                _products[i].Price = _products[i].Price * course;
            }
            return _products;
        }

        // või foreachina:
        // GET products/price-in-dollars2/1.5
        [HttpGet("price-in-dollars2/{course}")] 
        public List<Product> InDollars2(double course)
        {
            foreach (var p in _products)
            {
                p.Price = p.Price * course;
            }

            return _products;
        }

        // GET products/delete-all
        [HttpGet("delete-all")]
        public List<Product> DeleteAll()
        {
            _products.Clear();

            return _products;
        }
        // GET products/all-activity-to-false
        [HttpGet("all-activity-to-false")]
        public List<Product> ChangeAllActivityToFalse()
        {
            foreach (var p in _products)
            {
                p.IsActive= false;
            }

            return _products;
        }

        // GET products/product/2
        [HttpGet("product/{Num}")]
        public Product GetProductByNumber(int Num)
        {

            return _products.ElementAtOrDefault(Num);
        }


        // GET products/most-expensive
        [HttpGet("most-expensive")]
        public Product GetTheMostExpensiveProduct()
        {

            return _products.OrderByDescending(product => product.Price).FirstOrDefault();
        }

        // PUT products/update
        [HttpPut("update")]
        public ActionResult<Product> Update([FromBody] Product product)
        {
            Product updatingProduct = _products.FirstOrDefault(p => p.Id == product.Id);

            if (updatingProduct == null)
            {
                return NotFound(); 
            }
            updatingProduct.Name = product.Name;
            updatingProduct.Price = product.Price;
            updatingProduct.IsActive = product.IsActive;

            return Ok(updatingProduct);
        }

    }

}
