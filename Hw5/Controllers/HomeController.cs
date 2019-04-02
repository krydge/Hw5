using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hw5.Models;
using ServiceReference;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Hw5.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var client = new TextCasingSoapTypeClient(TextCasingSoapTypeClient.EndpointConfiguration.TextCasingSoap);
            var response = await client.InvertCaseFirstAdjustStringToCurrentAsync("hello World");
            ViewData["string"] = response.Body.InvertCaseFirstAdjustStringToCurrentResult;

                return View();
        }

        public async Task<IActionResult> Location(string address)
        {
            var locationClient = new HttpClient();
            var locationResponse = await locationClient.GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyAYX22qQW38R2hxpLej-3cEpmQJjluw0vw&address={address}&sensor=flase");
            var jObject = JObject.Parse(locationResponse);
            ViewData["name"] = (string)jObject["results"][0]["formatted_address"];
            ViewData["lat"] = (string)jObject["results"][0]["geometry"]["location"]["lat"];
            ViewData["lng"] = (string)jObject["results"][0]["geometry"]["location"]["lng"];

            var weatherClient = new HttpClient();
            string lat = (string)jObject["results"][0]["geometry"]["location"]["lat"];
            string lng = (string)jObject["results"][0]["geometry"]["location"]["lng"];
            var weatherResponce = await weatherClient.GetStringAsync($"https://api.darksky.net/forecast/bd1108cdcba19b14fc8324d4d7f2231d/{lat},{lng}");
            var jObject1 = JObject.Parse(weatherResponce);
            ViewData["TempData"] = (string)jObject1["currently"]["temperature"];
            ViewData["summary"] = (string)jObject1["daily"]["summary"];

            return View();
        }

        public async Task<IActionResult>InvertString(string invert)
        {
            var client = new TextCasingSoapTypeClient(TextCasingSoapTypeClient.EndpointConfiguration.TextCasingSoap);
            var responce = await client.InvertCaseFirstAdjustStringToCurrentAsync(invert ?? "Please enter a string next time");
            ViewData["string"] = responce.Body.InvertCaseFirstAdjustStringToCurrentResult;
            return View("invert");
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
