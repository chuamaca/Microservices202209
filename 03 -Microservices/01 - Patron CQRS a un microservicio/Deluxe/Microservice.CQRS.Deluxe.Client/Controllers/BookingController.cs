using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.CQRS.Deluxe.Client.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.CQRS.Deluxe.Client.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingService _service;
        public BookingController(BookingService service)
        {
            _service = service;
        }
        public IActionResult Add(int courtId, int length, int hour, string name)
        {
            _service.AddBooking(courtId, hour, length, name);
            return RedirectToAction("index", "home");
        }
    }
}