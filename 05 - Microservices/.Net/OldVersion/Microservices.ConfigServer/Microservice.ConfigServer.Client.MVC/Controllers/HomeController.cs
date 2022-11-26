using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microservices.ConfigServer.Client.MVC.Models;
using Microservices.ConfigServer.Client.MVC.Settings;
using Microsoft.Extensions.Options;

namespace Microservices.ConfigServer.Client.MVC.Controllers
{
    public class HomeController : Controller
    {        
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationSettings _applicationSettings;

        public HomeController(
            IOptions<ApplicationSettings> applicationSettings,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _applicationSettings = applicationSettings.Value;
        }

        public IActionResult Index()
        {
            return View(_applicationSettings);
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
