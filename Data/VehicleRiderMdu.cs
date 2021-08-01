using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data
{
    public class VehicleRiderMdu
    {
        public int Id { get; set; }
        public string ride_start_time { get; set; }
        public string ride_end_time { get; set; }
        public int shiftId { get; set; }
        public string adress_starting_point { get; set; }
        public string adress_destination { get; set; }
        public string GPS_starting_point { get; set; }
        public string GPS_destination { get; set; }
        public bool canceled { get; set; }
        public decimal price { get; set; }
    }
}
