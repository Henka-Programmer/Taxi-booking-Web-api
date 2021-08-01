using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data
{
    public class DriverModel
    {
       
        public string Username { get; set; }

        public string Email { get; set; }
        public string phoneNumber { get; set; }

        public IFormFile Photo { get; set; }

        public string birthDate { get; set; }

        public string DriverLicenceNumber { get; set; }

        public string ExpiryDate { get; set; }

        public bool IsWorking { get; set; }
    }
}
