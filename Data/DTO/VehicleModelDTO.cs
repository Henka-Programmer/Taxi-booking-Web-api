using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data
{
    public class VehicleModelDTO
    {
        [Required(ErrorMessage = "type is required")]
        public string type { get; set; }

        [Required(ErrorMessage = "model name is required")]
        public string model_name { get; set; }

        [Required(ErrorMessage = "License plate is required")]
        public string Licence_plate { get; set; }
        public string manufacture_year { get; set; }

        [Required(ErrorMessage = "Driver is required")]
        public int driverId { get; set; }    

        [Required(ErrorMessage = "Active feild is required")]
        public bool active { get; set; }
    }
}
