using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.CQRS.Premium.Client.Application;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.CQRS.Premium.Client.Controllers
{
    public class LiveController : Controller
    {
        private readonly LiveScoreApplicationService _liveScoreApplicationService;

        public LiveController(LiveScoreApplicationService liveScoreApplicationService)
        {
            _liveScoreApplicationService = liveScoreApplicationService;
        }
        public ActionResult Index()
        {
            var model = _liveScoreApplicationService.GetLiveViewModel();
            return View(model);
        }

        public PartialViewResult Update()
        {
            var model = _liveScoreApplicationService.GetLiveViewModel();
            return PartialView("_live", model);
        }
    }
}