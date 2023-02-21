using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using WebApplication1.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Weather")]
        [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Any)]
        public string Weather()
        {
            string strApiKey = "244871bd48c098405479a6e7e4971222"; // Should be stored more secure
            int iCnt = 40;
            //string strCityId = "2701680"; // Karlstad
            string strLon = "13.50357";
            string strLat = "59.379299";

            string strUrl = $"https://api.openweathermap.org/data/2.5/forecast?lat={strLat}&lon={strLon}&cnt={iCnt}&units=metric&lang=SE&appid={strApiKey}";
#if DEBUG
            Debug.WriteLine($"Url: {strUrl}");
#endif

            // HttpClient is intended to be instantiated once per application, rather than per-use.
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(strUrl).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        return content.ReadAsStringAsync().Result;
                    }
                }
            }
        }

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}