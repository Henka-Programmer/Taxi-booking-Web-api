using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Data.DTO
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string  type { get; set; }
        public string model_name { get; set; }
        public string Licence_plate { get; set; }
        public string manufacture_year { get; set; }
        public bool active { get; set; }
        public DriverForShiftsDTO driver { get; set; }
        public ICollection<ShiftDTO> shifts { get; set; }

       
    }
}
