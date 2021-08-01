using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VueJSDotnet51.Data;

namespace VueJSDotnet51.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string model_name { get; set; }
        public string  Licence_plate { get; set; }
        public string manufacture_year { get; set; }
        public Driver driver { get; set; }
        public bool active { get; set; }
        public ICollection<Shift> shifts { get; set; }

       
    }
}
