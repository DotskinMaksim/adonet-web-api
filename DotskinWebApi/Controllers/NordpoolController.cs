using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace DotskinWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NordpoolController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public NordpoolController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("{country}/{start}/{end}")]
        public async Task<IActionResult> GetNordPoolPrices(
             string country,
             string start,
             string end)
        {
            var response = await _httpClient.GetAsync(
                $"https://dashboard.elering.ee/api/nps/price?start={start}&end={end}");
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);

            var jsonDoc = JsonDocument.Parse(responseBody);
            var dataProperty = jsonDoc.RootElement.GetProperty("data");

            string prices;

            switch (country)
            {
                case "ee":
                    prices = dataProperty.GetProperty("ee").ToString();
                    Console.WriteLine(responseBody);

                    return Content(prices, "application/json");

                case "lv":
                    prices = dataProperty.GetProperty("lv").ToString();
                    return Content(prices, "application/json");

                case "lt":
                    prices = dataProperty.GetProperty("lt").ToString();
                    return Content(prices, "application/json");

                case "fi":
                    prices = dataProperty.GetProperty("fi").ToString();
                    return Content(prices, "application/json");

                default:
                    return BadRequest("Invalid country code.");
            }
        }
    }
}