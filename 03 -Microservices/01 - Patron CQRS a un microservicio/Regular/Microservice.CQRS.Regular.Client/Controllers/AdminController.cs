using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.CQRS.Regular.Client.Application;
using Microservice.CQRS.Regular.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.CQRS.Regular.Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminApplicationService _adminApplicationService;

        public AdminController(IAdminApplicationService service)
        {
            _adminApplicationService = service;
        }

        [HttpGet]
        [ActionName("Register")]
        public ActionResult DisplayRegister()
        {
            // Perform the query
            var model = _adminApplicationService.GetAdminViewModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult PostRegister(RegisterInputModel input)
        {
            // Perform the command
            _adminApplicationService.Register(input);

            // Re-routes
            return RedirectToAction("register");
        }
    }
}