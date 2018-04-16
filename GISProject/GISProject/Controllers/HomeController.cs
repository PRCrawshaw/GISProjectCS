using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GISProject.Models;
using System.Net.Http;

namespace GISProject.Controllers
{
    public class HomeController : Controller
    {
        public LocationModel locationData = new LocationModel();
        public string name;

        public IActionResult Index()
        {
            GetData();
            return View(locationData);
        }


        public async void GetData()
        {
            //We will make a GET request to a really cool website...

            string baseUrl = "http://localhost:3001/tests/1";
            //The 'using' will help to prevent memory leaks.
            //Create a new instance of HttpClient
            using (HttpClient client = new HttpClient())

            //Setting up the response...

            using (HttpResponseMessage res = await client.GetAsync(baseUrl))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    locationData.Name = data;
                }
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
