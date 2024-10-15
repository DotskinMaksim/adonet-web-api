using DotskinWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotskinWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static Product _product = new Product(1, "Koola", 1.5, true);

        // GET: product
        [HttpGet]
        public Product GetToode()
        {
            return _product;
        }

        // GET: product/increase-price
        [HttpGet("increase-price")]
        public Product IncreasePrice()
        {
            _product.Price = _product.Price + 1;
            return _product;
        }
        // GET: product/change-activity
        [HttpGet("change-activity")]
        public Product ChangeActivity()
        {
            _product.IsActive = !_product.IsActive;
            return _product;
        }

        // GET: product/change-name/testname
        [HttpGet("change-name/{name}")]
        public Product ChangeName(string Name)
        {
            _product.Name = Name;
            return _product;
        }

        // GET: product/multiply-price/3
        [HttpGet("multiply-price/{num}")]
        public Product MultiplyPrice(int Num)
        {
            _product.Price = _product.Price*Num;
            return _product;
        }
    }
}
