using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment _environment;

        public FileManager(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public string SaveFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\upload"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\upload");
                }

                using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\upload\\" + file.FileName))
                {
                    file.CopyTo(filestream);
                    filestream.Flush();

                   
                }

                return "/upload/" + file.FileName;
            }

            return String.Empty;
        }
    }
}
