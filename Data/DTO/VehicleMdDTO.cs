using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data.DTO
{
    public class VehicleMdDTO
    {
        public int Id { get; set; }
        public string model_name { get; set; }
        public string model_description { get; set; }

        public ICollection<VehicleForVehicleMdDTO> vehicles { get; set; }
    }
}
