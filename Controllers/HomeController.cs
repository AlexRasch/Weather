using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly string weatherApiKey;
        public HomeController(ILogger<HomeController> logger,
                              IConfiguration config)
        {
            _logger = logger;
            _config = config;

            weatherApiKey = _config["Weather:ServiceApiKey"];
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Weather")]
        [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Any)]
        public string Weather()
        {
            int iCnt = 40;
            string strLon = "13.50357";
            string strLat = "59.379299";

            string strUrl = $"https://api.openweathermap.org/data/2.5/forecast?lat={strLat}&lon={strLon}&cnt={iCnt}&units=metric&lang=SE&appid={weatherApiKey}";
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}