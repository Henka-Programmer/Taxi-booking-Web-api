using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Helpers
{
    public static class Extensions
    {

        
        public static string GetAge(this string birthdate)
        {
            var ParsedbirthDate = DateTime.ParseExact(birthdate,"dd/MM/yyyy", CultureInfo.InvariantCulture);
            var age = DateTime.Now.Year - ParsedbirthDate.Year;

            return age.ToString();
            
        }


      public static bool IsValidStringDate(string stringDate)
      {
            DateTime parsed;
            bool valid = DateTime.TryParseExact(stringDate, "dd/MM/yyyy",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None, out parsed);
            return valid;
      }

      public async static Task<ImageUploadResult> UploadImage(IFormFile file)
      {
            var cloudinary = new Cloudinary("cloudinary://847954438113788:olMCg637dmyWb8nr6IojxPGZIW0@taxiservapp");

            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                // The formFile is the method parameter which type is IFormFile
                // Saves the files to the local file system using a file name generated by the app.
                await file.CopyToAsync(stream);
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath)
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult;
      }


    }
}
