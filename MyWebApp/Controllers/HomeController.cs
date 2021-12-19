using Microsoft.AspNetCore.Mvc;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MyWebApp.Common;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Hosting;
using MyWebApp.Models.File;
using System.IO;
using Microsoft.Extensions.Localization;
using System.Web;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly SendGridConfiguration _sendGridConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer _stringLocalizer;
        private IConfiguration Configuration { get; }

        public HomeController(ILogger<HomeController> logger, 
            IEmailSender emailSender, 
            IHttpContextAccessor httpContextAccessor, 
            IOptionsMonitor<SendGridConfiguration> options,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _sendGridConfig = options.CurrentValue;
            _webHostEnvironment = webHostEnvironment;
        }

        void LogMe()
        {
            var currentUrl = _httpContextAccessor.HttpContext.Request.GetEncodedUrl();
            var curentiP = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            _logger.LogInformation($"\n---Curent Url: {currentUrl}  ---Time: {DateTime.UtcNow} ---IP-address: {curentiP}");
            
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) }
                );
            return LocalRedirect(returnUrl);
        }
        public virtual ActionResult FilesListPage()
        {
            var uploadedFiles = new FilesList();
            string result="";
            string filesDirectory = $"{_webHostEnvironment.WebRootPath}\\Files";
            var files = Directory.GetFiles(filesDirectory);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                var uploadedFile = new UploadFileViewModel() { Name = Path.GetFileName(file), fileDirectory=filesDirectory +"\\"+ Path.GetFileName(file) };

                uploadedFiles.FileList.Add(uploadedFile);
            }
            //foreach (var p in uploadedFiles)
            //    result = $"{result}<a href=\"{p.fileDirectory}\">{p.Name}</a><br>";
            
            return View(uploadedFiles);
        }
        public FileResult Download(string file)
        {
            var doc = new byte[0];
            string filename = file.Split("\\").Last();
            doc = System.IO.File.ReadAllBytes(file);
            return File(doc, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
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
            if (!ModelState.IsValid)
            {
                return View("Contact");
            }
            _emailSender.Key = _sendGridConfig.ApiKey;
            _emailSender.EmailMy = _sendGridConfig.EmailFrom;
            await _emailSender.SendMessage(vm.EmailTo, vm.EmailMessage, vm.EmailBody);
            
            return View("Contact");
        }
    }
}
