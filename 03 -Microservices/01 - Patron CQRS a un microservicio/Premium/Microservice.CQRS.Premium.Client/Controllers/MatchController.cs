using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.CQRS.Premium.Client.Application;
using Microservice.CQRS.Premium.Command.Model;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.CQRS.Premium.Client.Controllers
{
    public class MatchController : Controller
    {
        private readonly MatchApplicationService _matchApplicationService;

        public MatchController(MatchApplicationService matchApplicationService)
        {
            _matchApplicationService = matchApplicationService; 
        }
        public ActionResult Index(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return RedirectToAction("index", "home");

            var model = _matchApplicationService.GetMatchDetails(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Action(string id, EventType whatHappened)
        {
            _matchApplicationService.ProcessAction(id, whatHappened);
            return RedirectToAction("index", new { id = id });
        }
    }
}