using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.CQRS.Premium.Client.Application;
using Microservice.CQRS.Premium.Client.Common.Actions;
using Microservice.CQRS.Premium.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.CQRS.Premium.Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminApplicationService _adminApplicationService;

        public AdminController(AdminApplicationService adminApplicationService)
        {
            _adminApplicationService = adminApplicationService;
        }
        public ActionResult Index()
        {
            var model = new ViewModelBase();
            return View(model);
        }

        [HttpPost]
        public ActionResult Action(AdminAction action)
        {
            _adminApplicationService.ProcessAction(action);
            return RedirectToAction("index", "home");
        }
    }
}