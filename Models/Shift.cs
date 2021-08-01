using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VueJSDotnet51.Data;

namespace VueJSDotnet51.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public Driver Driver { get; set; }
        public int DriverId { get; set; }
        public Vehicle vehicle { get; set; }
        public int VehicleId { get; set; }
        public ICollection<Vehicle_ride> vehicle_Rides { get; set; }
       
        public ICollection<Vehicle_ride_status> Vehicle_Ride_Statuses { get; set; }
    }
}
