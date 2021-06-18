using CalendarProj.BL.Services.Interfaces;
using CalendarProj.DAO.Models;
using CalendarProj.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Vision.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CalendarProj.Controllers
{
    public class HomeController : Controller
    {       
        private readonly ILogger<HomeController> _logger;

        private readonly IGoogleCalendarService _googleCalendarService;

        public HomeController(ILogger<HomeController> logger,

            IGoogleCalendarService googleCalendarService)
        {
            _logger = logger;

            _googleCalendarService = googleCalendarService;
        }

        public IActionResult Index()
        {
            var events = _googleCalendarService.GetEvents();
            ViewBag.Events = events;
            return View();             
        }

        public IActionResult Create()
        {
            CustomEvent customEvent = new CustomEvent()
            {
                Types = _googleCalendarService.GetAllTypes()
            };
            return View(customEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomEvent customEvent)
        {
            if (ModelState.IsValid)
            {
                if (_googleCalendarService.IsFreeTime(customEvent))
                {
                    _googleCalendarService.CreateEvent(customEvent);

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessageDate = "This date is busy";
                }
            }

            customEvent.Types = _googleCalendarService.GetAllTypes();
            return View(customEvent);
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
