using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueJSDotnet51.Data;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftRepository _repo;
        private readonly ApiContext _ctx;

        public ShiftController(IShiftRepository repo, ApiContext ctx)
        {
            _repo = repo;
            _ctx = ctx;
        }


        [HttpGet]
        public IActionResult getshifts()
        {
            var shifts = _repo.Getshifts();
            return Ok(shifts);
        }


        [HttpGet("driver/{id}")]
        public IActionResult getshiftByDriverId(int id)
        {
            var shifts = _repo.GetshiftsByDriverId(id);

            return Ok(shifts);
        }




        [HttpPost]
        public async Task<IActionResult> Addshift(ShiftModel shiftmodel)
        {

            var driver = _repo.GetDriverById(shiftmodel.driverid);
            var vehicle = _repo.GetVehicleById(shiftmodel.vehicleid);

            if (driver != null && vehicle != null)
            {
                var shift = new Shift() { Driver = driver, vehicle = vehicle, start_time = shiftmodel.start_time, end_time = shiftmodel.end_time };

                await _repo.Addshift(shift);


                if (await _repo.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest("No such record found!");
            }

            return BadRequest("something went wrong!");

        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Deleteshift(int id)
        {
            _repo.DeleteShift(id);

            if (await _repo.SaveChanges() > 0)
            {
                return Ok();
            }

            return BadRequest();

        }


        [HttpGet("count")]
        public IActionResult shiftsCount()
        {
            var count = _ctx.shfits.Count();
            return Ok(new { NumberOfShifts = count});
        }
    }
}