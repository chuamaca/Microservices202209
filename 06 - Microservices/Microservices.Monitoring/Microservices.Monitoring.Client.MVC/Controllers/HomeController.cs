using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microservices.Monitoring.Client.MVC.Models;
using Microservices.Monitoring.Client.MVC.Application;

namespace Microservices.Monitoring.Client.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApplicationService _productApplicationService;

        public HomeController(ILogger<HomeController> logger,IProductApplicationService productApplicationService)
        {
            _logger = logger;
            _productApplicationService = productApplicationService;
        }

        public IActionResult Index()
        {
            var products = _productApplicationService.ListProducts();
            return View(products);
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
