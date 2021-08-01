using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data.DTO
{
    public class DriverDTO
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string DriverLicenceNumber { get; set; }
        public string ExpiryDate { get; set; }
        public bool IsWorking { get; set; }
        public string age { get; set; }
        public string photo { get; set; }
        public List<ShiftDTO> shifts { get; set; }

    }
}
