using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microservices.Resilient.Hystrix.Client.MVC.Models;
using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Agents.Orders;
using Microservices.Resilient.Hystrix.Client.MVC.Infrastructure.Data.Entities;

namespace Microservices.Resilient.Hystrix.Client.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrdersAgent _ordersAgents;

        public HomeController(ILogger<HomeController> logger,IOrdersAgent ordersAgents)
        {
            _logger = logger;
            _ordersAgents = ordersAgents;
        }

        public async Task<IActionResult> Index()
        {
            List<Order> orders = await _ordersAgents.ListAsync();
            Order order = await _ordersAgents.GetAsync(1);
            return View(orders);
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
