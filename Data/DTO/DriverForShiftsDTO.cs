using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data.DTO
{
    public class DriverForShiftsDTO
    {
        public int Id { get; set; }
        public string uid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }
        public string DriverLicenceNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string birthDate { get; set; }
        public bool IsWorking { get; set; }
    }
}
