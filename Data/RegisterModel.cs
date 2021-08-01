using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string  Password { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string role { get; set; }

        public IFormFile Photo { get; set; }
    }
}
