using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace VueJSDotnet51.Models
{
    public class Rider 
    {
        public int Id { get; set; }
        public string uid { get; set; }
        public string Username { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string Photo { get; set; }

        ICollection<Vehicle_ride_status> vehicle_Ride_Statuses;

    }
}
