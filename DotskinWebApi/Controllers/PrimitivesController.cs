using Microsoft.AspNetCore.Mvc;

namespace DotskinWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimitivesController : ControllerBase
    {

        // GET: primitives/hello-world
        [HttpGet("hello-world")]
        public string HelloWorld()
        {
            return "Tere päevast, praegu on " + DateTime.Now;
        }

        // GET: primitives/hello-variable/mari
        [HttpGet("hello-variable/{name}")]
        public string HelloVariable(string name)
        {
            return "Tere " + name;
        }

        // GET: primitives/add/5/6
        [HttpGet("add/{nr1}/{nr2}")]
        public int AddNumbers(int nr1, int nr2)
        {
            return nr1 + nr2;
        }

        // GET: primitives/multiply/5/6
        [HttpGet("multiply/{nr1}/{nr2}")]
        public int Multiply(int nr1, int nr2)
        {
            return nr1 * nr2;
        }

        // GET: primitives/do-logs/5
        [HttpGet("do-logs/{amount}")]
        public void DoLogs(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine("See on logi nr " + i);
            }
        }
        // GET: primitives/rand-num/5/10
        [HttpGet("rand-num/{nr1}/{nr2}")]
        public int RandNum(int nr1, int nr2)
        {
            Random random= new Random();
            return random.Next(nr1, nr2);
        }
        // GET: primitives/birthday/2006/01
        [HttpGet("birthday/{year}/{month}")]
        public string Birthday(int year, int month)
        {
            int years = (month < DateTime.Now.Month) ? DateTime.Now.Year - year : DateTime.Now.Year - year - 1;
            return "Olete " + years.ToString() + " aastat vana";
        }
    }
}