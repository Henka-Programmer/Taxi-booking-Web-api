using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data
{
    public class VehicleRidersMd
    {
        [Required(ErrorMessage = "ride start time is required")]
        public string ride_start_time { get; set; }
        public string ride_end_time { get; set; }

        [Required(ErrorMessage ="Shift Id is required")]
        public int shiftId { get; set; }

        [Required(ErrorMessage = "adress starting point is required")]
        public string adress_starting_point { get; set; }

        [Required(ErrorMessage = "adress destination is is required")]
        public string adress_destination { get; set; }

        [Required(ErrorMessage = "GPS starting point required")]
        public string GPS_starting_point { get; set; }

        [Required(ErrorMessage = "GPS destination is required")]
        public string GPS_destination { get; set; }

        [Required(ErrorMessage = "Canceled feild is required")]
        public bool canceled { get; set; }
        public decimal price { get; set; }
    }
}
