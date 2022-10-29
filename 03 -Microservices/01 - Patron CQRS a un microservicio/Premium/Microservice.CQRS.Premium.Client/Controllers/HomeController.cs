using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microservice.CQRS.Premium.Client.Models;
using Microservice.CQRS.Premium.Client.Application;

namespace Microservice.CQRS.Premium.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeApplicationService _homeApplicationService;

        public HomeController(HomeApplicationService homeApplicationService)
        {
            _homeApplicationService = homeApplicationService;
        }

        public ActionResult Index()
        {
            var model = _homeApplicationService.GetIndexViewModel();
            return View(model);
        }
    }
}
