using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data
{
    public class RegisterDriverModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string phoneNumber { get; set; }

       
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "birthdate is required")]
        public string birthDate { get; set; }

        [Required(ErrorMessage = "Driver Licence Number is required")]
        public string DriverLicenceNumber { get; set; }

        [Required(ErrorMessage = "Expiry Date is required")]
        public string ExpiryDate { get; set; }

        public bool IsWorking { get; set; }
    }
}
