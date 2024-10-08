using DotskinWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotskinWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToodeController : ControllerBase
    {
        private static Toode _toode = new Toode(1, "Koola", 1.5, true);

        // GET: toode
        [HttpGet]
        public Toode GetToode()
        {
            return _toode;
        }

        // GET: toode/suurenda-hinda
        [HttpGet("suurenda-hinda")]
        public Toode SuurendaHinda()
        {
            _toode.Price = _toode.Price + 1;
            return _toode;
        }
        // GET: toode/change-activity
        [HttpGet("change-activity")]
        public Toode ChangeActivity()
        {
            _toode.IsActive = !_toode.IsActive;
            return _toode;
        }

        // GET: toode/change-name/testname
        [HttpGet("change-name/{name}")]
        public Toode ChangeName(string Name)
        {
            _toode.Name = Name;
            return _toode;
        }

        // GET: toode/multiply-price/3
        [HttpGet("multiply-price/{num}")]
        public Toode MultiplyPrice(int Num)
        {
            _toode.Price = _toode.Price*Num;
            return _toode;
        }
    }
}
