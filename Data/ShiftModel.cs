using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data
{
    public class ShiftModel
    {
        [Required(ErrorMessage = "Driver Id is requried")]
        public int driverid { get; set; }

        [Required(ErrorMessage = "Vehicle Id is requried")]
        public int vehicleid { get; set; }

        [Required]
        public string start_time { get; set; }

        [Required]
        public string end_time { get; set; }

    }
}
