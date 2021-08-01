using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Data.DTO
{
    public class ShiftforShiftDTO
    {
        public int Id { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public DriverForShiftsDTO driver { get; set; }

        public VehicleforShiftDTO vehicle { get; set; }
    }
}
