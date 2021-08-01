using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Data
{
    public class Driver
    {
        public int Id { get; set; }
        public string uid { get; set; }
        public string  Username { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }
        public string DriverLicenceNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string birthDate { get; set; }
        public bool IsWorking { get; set; }
        public string Photo { get; set; }
        public ICollection<Shift> shifts { get; set; }
    }
}
