using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Data.DTO
{
    public class VehicleRideDTO
    {
        public int Id { get; set; }
        public string ride_start_time { get; set; }
        public string ride_end_time { get; set; }
        public ShiftDTO shift { get; set; }
        public string adress_starting_point { get; set; }
        public string adress_destination { get; set; }
        public string GPS_starting_point { get; set; }
        public string GPS_destination { get; set; }
        public bool canceled { get; set; }
        public decimal price { get; set; }
        
    }
}
