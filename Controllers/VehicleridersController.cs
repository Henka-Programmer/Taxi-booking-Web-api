using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJSDotnet51.Data;
using VueJSDotnet51.Data.DTO;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleridersController : ControllerBase
    {
        private readonly ApiContext _ctx;
        private readonly IMapper _mapper;

        public VehicleridersController(ApiContext ctx, IMapper mapper)
        {
            _ctx = ctx;
           _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleRiders()
        {
            var vr = await _ctx.vehicle_Rides.ToListAsync();
            var res = _mapper.Map<List<VehicleRideDTO>>(vr);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehcileRideById(int id)
        {
            var vr = await _ctx.vehicle_Rides.FindAsync(id);

            if (vr == null)
            {
                return BadRequest("No such record with that Id");

            }else
            {
                var res = _mapper.Map<VehicleRideDTO>(vr);
                return Ok(res);
            }

           
        }


        [HttpPost]
        public async Task<IActionResult> AddVehicleRiders(VehicleRidersMd model)
        {
            var shift = _ctx.shfits.Find(model.shiftId);

            if(shift == null)
            {
                return BadRequest("No such shift is found!, please enter a valid Shift ID");
            }else
            {
                Vehicle_ride vr = new Vehicle_ride
                {
                    adress_destination = model.adress_destination,
                    adress_starting_point = model.adress_starting_point,
                    GPS_destination = model.GPS_destination,
                    GPS_starting_point = model.GPS_starting_point,
                    ride_start_time = model.ride_start_time,
                    ride_end_time = model.ride_end_time,
                    price = model.price,
                    canceled = model.canceled,
                    shift = shift,
                };

                _ctx.vehicle_Rides.Add(vr);

                if (await _ctx.SaveChangesAsync() > 0)
                {
                    return Ok();
                }
            }

            return BadRequest();

        }




        [HttpPut]
        public async Task<IActionResult> UpdateVehicleRiders(VehicleRiderMdu model)
        {
            var shift = _ctx.shfits.Find(model.shiftId);
            var vr = _ctx.vehicle_Rides.Find(model.Id);

            if (shift != null && vr != null)
            {

                vr.adress_destination = model.adress_destination;
                vr.adress_starting_point = model.adress_starting_point;
                vr.GPS_destination = model.GPS_destination;
                vr.GPS_starting_point = model.GPS_starting_point;
                vr.ride_start_time = model.ride_start_time;
                vr.ride_end_time = model.ride_end_time;
                vr.price = model.price;
                vr.canceled = model.canceled;
                vr.shift = shift;
             
                if (await _ctx.SaveChangesAsync() > 0)
                {
                    return Ok();
                }
            }else
            {
                return BadRequest(" No such data found with that Id!");
            }

            return BadRequest("somthing went wrong");

        }
    }
}
