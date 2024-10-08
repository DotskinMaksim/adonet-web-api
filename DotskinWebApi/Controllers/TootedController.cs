using DotskinWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotskinWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TootedController : ControllerBase
    {
        private static List<Toode> _tooted = new()
        {
        new Toode(1,"Koola", 1.5, true),
        new Toode(2,"Fanta", 1.0, false),
        new Toode(3,"Sprite", 1.7, true),
        new Toode(4,"Vichy", 2.0, true),
        new Toode(5,"Vitamin well", 2.5, true)
        };


        // GET: tooted
        [HttpGet]
        public List<Toode> Get()
        {
            return _tooted;
        }

        // GET: tooted/kustuta/0
        [HttpGet("kustuta/{index}")]
        public List<Toode> Delete(int index)
        {
            _tooted.RemoveAt(index);
            return _tooted;
        }

        // GET: tooted/kustuta2/0
        [HttpGet("kustuta2/{index}")]
        public string Delete2(int index)
        {
            _tooted.RemoveAt(index);
            return "Kustutatud!";
        }

        // GET: tooted/lisa/6/testtoode/5.0/true
        [HttpGet("lisa/{id}/{nimi}/{hind}/{aktiivne}")]
        public List<Toode> Add(int id, string nimi, double hind, bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }

        // GET tooted/lisa?id=1&nimi=piim&hind=4.5&aktiivne=false
        [HttpGet("lisa")] 
        public List<Toode> Add2([FromQuery] int id, [FromQuery] string nimi, [FromQuery] double hind, [FromQuery] bool aktiivne)
        {
            Toode toode = new Toode(id, nimi, hind, aktiivne);
            _tooted.Add(toode);
            return _tooted;
        }


        // GET tooted/hind-dollaritesse/1.5
        [HttpGet("hind-dollaritesse/{kurss}")]
        public List<Toode> Dollaritesse(double kurss)
        {
            for (int i = 0; i < _tooted.Count; i++)
            {
                _tooted[i].Price = _tooted[i].Price * kurss;
            }
            return _tooted;
        }

        // või foreachina:
        // GET tooted/hind-dollaritesse2/1.5
        [HttpGet("hind-dollaritesse2/{kurss}")] 
        public List<Toode> Dollaritesse2(double kurss)
        {
            foreach (var t in _tooted)
            {
                t.Price = t.Price * kurss;
            }

            return _tooted;
        }

        // GET tooted/delete-all
        [HttpGet("delete-all")]
        public List<Toode> DeleteAll()
        {
            _tooted.Clear();

            return _tooted;
        }
        // GET tooted/all-activity-to-false
        [HttpGet("all-activity-to-false")]
        public List<Toode> ChangeAllActivityToFalse()
        {
            foreach (var t in _tooted)
            {
                t.IsActive= false;
            }

            return _tooted;
        }
        // GET tooted/toode/2
        [HttpGet("toode/{id}")]
        public Toode GetToodeById(int Id)
        {
  
            return _tooted.FirstOrDefault(toode => toode.Id == Id);
        }
        // GET tooted/most-expensive
        [HttpGet("most-expensive")]
        public Toode GetTheMostExpensiveToode()
        {

            return _tooted.OrderByDescending(toode => toode.Price).FirstOrDefault();
        }
    }

}
