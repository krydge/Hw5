using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hw5.Models;
using ServiceReference;
using ServiceReference1;

namespace Hw5.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var client = new TextCasingSoapTypeClient(TextCasingSoapTypeClient.EndpointConfiguration.TextCasingSoap);
            var response = await client.InvertCaseFirstAdjustStringToCurrentAsync("hello World");
            ViewData["string"] = response.Body.InvertCaseFirstAdjustStringToCurrentResult;


            var weatherClient = new WeatherSoapClient(WeatherSoapClient.EndpointConfiguration.WeatherSoap);
            var weatherResponse = await weatherClient.GetCityForecastByZIPAsync("84627");
            ViewData["weather"] = weatherResponse.;
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
