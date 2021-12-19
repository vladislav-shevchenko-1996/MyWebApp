using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Models.File
{
    public class FilesList
    {
        public List<UploadFileViewModel> FileList { get; set; }
        public FilesList()
        {
            FileList = new List<UploadFileViewModel>();
        }
    }
}
