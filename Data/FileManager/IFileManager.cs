using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data
{
    public interface IFileManager
    {
        string SaveFile(IFormFile file);
    }
}
