using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Models
{
    public class Vehicle_ride_status
    {
        public int Id { get; set; }
        public string status_time { get; set; }
        public string status_detail { get; set; }
        public Shift shift { get; set; }
        public Vehicle_ride vehicle_Ride { get; set; }
        public Rider rider { get; set; }
    }
}
