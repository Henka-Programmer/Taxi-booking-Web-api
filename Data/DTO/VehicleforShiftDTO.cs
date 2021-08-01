using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data.DTO
{
    public class VehicleforShiftDTO
    {
        public int Id { get; set; }
        public string Licence_plate { get; set; }

        public string manufacture_year { get; set; }
        public bool active { get; set; }
    }
}
