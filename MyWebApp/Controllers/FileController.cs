using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Controllers
{
    
    public class FileController: Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        


        [HttpPost]
        public async Task<IActionResult> Upload(UploadFileViewModel vm)
        {
            string originalFileName = vm.File.FileName;
            string fileExtension = originalFileName.Split(".").Last();
            string newFileName = $"{vm.Name}.{fileExtension}";
            string filesDirectory = $"{_webHostEnvironment.WebRootPath}\\Files";
            if (!Directory.Exists(filesDirectory))
            {
                Directory.CreateDirectory(filesDirectory);
            }
            string filePath = $"{filesDirectory}\\{newFileName}";
            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await vm.File.CopyToAsync(fileStream);
            }
            return Content("File uploaded succesfully");
        }
    }
}
