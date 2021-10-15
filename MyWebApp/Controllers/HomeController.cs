using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApp.Services.EmailSender;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutCompany()
        {
            return View();
        }
        public IActionResult Pricing()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Portfolio()
        {
            return View();
        }

        public IActionResult BlogGrid()
        {
            return View();
        }

        public IActionResult BlogSidebar()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
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
        [HttpPost]
        public async Task<IActionResult> SendEMail([FromForm] SendEmailMessageVm vm)
        {
            await _emailSender.SendMessage(vm.EmailTo, vm.EmailMessage, vm.EmailBody);
            return Json(new { success = true });
        }
    }
}
