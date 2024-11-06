using DotskinWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotskinWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static Product _product = new Product(1, "Koola", 1.5, true , "item", true, "", 200, "Joogid");

        // GET: product
        [HttpGet]
        public Product GetProduct()
        {
            return _product;
        }

        // GET: product/increase-price
        [HttpGet("increase-price")]
        public Product IncreasePrice()
        {
            _product.PricePerUnit = _product.PricePerUnit + 1;
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
        [HttpGet("change-name/{Name}")]
        public Product ChangeName(string Name)
        {
            _product.Name = Name;
            return _product;
        }

        // GET: product/multiply-price/3
        [HttpGet("multiply-price/{Num}")]
        public Product MultiplyPrice(int Num)
        {
            _product.PricePerUnit = _product.PricePerUnit *Num;
            return _product;
        }
    }
}
