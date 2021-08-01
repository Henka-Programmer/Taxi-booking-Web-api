using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJSDotnet51.Data;
using VueJSDotnet51.Data.DTO;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ApiContext _ctx;
        private readonly IMapper _mapper;

        public VehicleController(ApiContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles = await _ctx.vehicles.Include(a => a.shifts).Include(v => v.driver).ToListAsync();
            var response = _mapper.Map<List<VehicleDTO>>(vehicles);
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicles(int id)
        {
            var vehicle = await _ctx.vehicles.Include(a => a.shifts).Include(v => v.driver).SingleOrDefaultAsync(x => x.Id == id);
            var response = _mapper.Map<VehicleDTO>(vehicle);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddVehicle(VehicleModelDTO model)
        {

            var driver = _ctx.drivers.Find(model.driverId);

            if(driver != null)
            {
                var res = new Vehicle
                {
                    type = model.type,
                    model_name = model.model_name,
                    Licence_plate = model.Licence_plate,
                    manufacture_year = model.manufacture_year,
                    active = model.active,
                    driver = driver
                    
                };


                _ctx.vehicles.Add(res);

            }


            if (await _ctx.SaveChangesAsync() > 0)
            {
                return Ok("Added succesfully!");

            }

            return BadRequest();
        }


    }
}
