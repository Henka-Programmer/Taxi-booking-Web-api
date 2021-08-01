using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJSDotnet51.Data;
using VueJSDotnet51.Data.DTO;
using VueJSDotnet51.Models;
using VueJSDotnet51.Helpers;

namespace VueJSDotnet51.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Driver, DriverForShiftsDTO>();
            
            CreateMap<Vehicle_ride, VehicleRideDTO>();
            CreateMap<Vehicle, VehicleDTO>();
            CreateMap<Vehicle, VehicleForVehicleMdDTO>();

            CreateMap<Shift, ShiftDTO>();
            CreateMap<Shift, ShiftforShiftDTO>();
            CreateMap<Vehicle, VehicleforShiftDTO>();
            CreateMap<Rider, RiderDTO>();
            CreateMap<Driver, DriverDTO>().ForMember(dest => dest.age, opt => { opt.MapFrom(s => s.birthDate.GetAge()); });
          
        }
    }
}
