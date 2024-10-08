using Microsoft.AspNetCore.Mvc;

namespace DotskinWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrimitiividController : ControllerBase
    {

        // GET: primitiivid/hello-world
        [HttpGet("hello-world")]
        public string HelloWorld()
        {
            return "Hello world at " + DateTime.Now;
        }

        // GET: primitiivid/hello-variable/mari
        [HttpGet("hello-variable/{nimi}")]
        public string HelloVariable(string nimi)
        {
            return "Hello " + nimi;
        }

        // GET: primitiivid/add/5/6
        [HttpGet("add/{nr1}/{nr2}")]
        public int AddNumbers(int nr1, int nr2)
        {
            return nr1 + nr2;
        }

        // GET: primitiivid/multiply/5/6
        [HttpGet("multiply/{nr1}/{nr2}")]
        public int Multiply(int nr1, int nr2)
        {
            return nr1 * nr2;
        }

        // GET: primitiivid/do-logs/5
        [HttpGet("do-logs/{arv}")]
        public void DoLogs(int arv)
        {
            for (int i = 0; i < arv; i++)
            {
                Console.WriteLine("See on logi nr " + i);
            }
        }
        // GET: primitiivid/rand-num/5/10
        [HttpGet("rand-num/{nr1}/{nr2}")]
        public int RandNum(int nr1, int nr2)
        {
            Random random= new Random();
            return random.Next(nr1, nr2);
        }
        // GET: primitiivid/birthday/2006/01
        [HttpGet("birthday/{year}/{month}")]
        public string Birthday(int year, int month)
        {
            int years = (month < DateTime.Now.Month) ? DateTime.Now.Year - year : DateTime.Now.Year - year - 1;
            return "Olete " + years.ToString() + " aastat vana";
        }
    }
}