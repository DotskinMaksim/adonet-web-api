using Microsoft.AspNetCore.Mvc;

namespace DotskinWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelMachineController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ParcelMachineController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //ParcelMachine/omniva
        [HttpGet]
        public async Task<IActionResult> GetOmnivaParcelMachines()
        {
            var response = await _httpClient.GetAsync("https://www.omniva.ee/locations.json");
            var responseBody = await response.Content.ReadAsStringAsync();
            return Content(responseBody, "application/json");
        }
        ////ParcelMachine/smartpost
        //[HttpGet("smartpost")]
        //public async Task<IActionResult> GetSmartpostParcelMachines()
        //{
        //    // Küsime andmed SmartPost API-st
        //    var response = await _httpClient.GetAsync("https://www.smartpost.ee/places.json");
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    return Content(responseBody, "application/json");
        //}
    }
}
