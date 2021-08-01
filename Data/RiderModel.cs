using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data
{
    public class RiderModel
    {
        [Required]
        public string Id { get; set; }
        public string username { get; set; }
        public string phoneNumber { get; set; }
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
    }
}
