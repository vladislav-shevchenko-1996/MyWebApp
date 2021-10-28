﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApp.Services.EmailSender;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, 
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        void LogMe()
        {
            //var currentUrl = HttpContext.Request.GetEncodedUrl();
            var currentUrl = _httpContextAccessor.HttpContext.Request.GetEncodedUrl();
            //var curentIP = _httpContextAccessor.HttpContext.Connection.LocalIpAddress.MapToIPv4();
            var curentiP = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            _logger.LogInformation($"\n---Curent Url: {currentUrl}  ---Time: {DateTime.UtcNow} ---IP-address: {curentiP}");
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    _logger.LogInformation($"HomeController->Index->Exception: {ex.Message} ---IP-addres: {curentIP}");
            //}
        }
        public IActionResult Index()
        {
            LogMe();
            return View();
        }

        public IActionResult AboutCompany(int userId, string country)
        {
            LogMe();
            return View();
        }
        public IActionResult Pricing()
        {
            LogMe();
            return View();
        }

        public IActionResult Service()
        {
            LogMe();
            return View();
        }
        public IActionResult Portfolio()
        {
            LogMe();
            return View();
        }

        public IActionResult BlogGrid()
        {
            LogMe();
            return View();
        }

        public IActionResult BlogSidebar()
        {
            LogMe();
            return View();
        }

        public IActionResult BlogSingle()
        {
            LogMe();
            return View();
        }

        public IActionResult Contact()
        {
            LogMe();
            return View();
        }

        public IActionResult Privacy()
        {
            LogMe();
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
