using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueJSDotnet51.Data;
using VueJSDotnet51.Data.DTO;
using FirebaseAdmin;
using System.Security.Claims;

namespace VueJSDotnet51.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DriverController : ControllerBase
    {
        private readonly ApiContext _ctx;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;

        public DriverController(ApiContext ctx,IMapper mapper, IFileManager fileManager)
        {
            _ctx = ctx;
            _mapper = mapper;
            _fileManager = fileManager;
        }

        
        [HttpGet]
        public IActionResult GetDrivers()
        {

            var drivers = _ctx.drivers.Include(a=>a.shifts).ToList();
            var res = _mapper.Map<List<DriverDTO>>(drivers);
           
            return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriverById(int id)
        {
            var driver = await _ctx.drivers.FirstOrDefaultAsync(x=> x.Id == id);
            var res = _mapper.Map<DriverDTO>(driver);
             return Ok(res);
        }



        [HttpGet("{uid}")]
        public async Task<IActionResult> GetDriverByuid(string uid)
        {
            var driver = await _ctx.drivers.FirstOrDefaultAsync(x => x.uid == uid);
            var res = _mapper.Map<DriverDTO>(driver);
            return Ok(res);
        }


        [HttpGet("currentdriver")]
        public async Task<IActionResult> GetCurrentDriver()
        {
            
            var uid = User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            var driver = await _ctx.drivers.FirstOrDefaultAsync(x => x.uid == uid);
            var res = _mapper.Map<DriverDTO>(driver);
            return Ok(res);
        }


        [HttpGet("count")]
        
        public IActionResult CountDrivers()
        {
            var count = _ctx.drivers.Count();

            return Ok(new { Drivers = count});
        }


        [HttpGet("Working")]
        public IActionResult WorkingDrivers()
        {
            var WorkingDrivers = _ctx.drivers.Where(x=>x.IsWorking == true).Count();

            return Ok(new { NumberOfWorkingDrivers = WorkingDrivers});
        }

        
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteDriver(int userid)
        {
            var driver = _ctx.drivers.Find(userid);
            _ctx.drivers.Remove(driver);

            await FirebaseAuth.DefaultInstance.DeleteUserAsync(driver.uid);

            if (await _ctx.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        
        [HttpPut]
        [Authorize(Policy = "Driver")]
        public async Task<IActionResult> UpdateDriver([FromForm] DriverModel model)
        {
            //save photo
            var photoPath = _fileManager.SaveFile(model.Photo);


            var userid = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            UserRecordArgs args = new UserRecordArgs()
            {
                Uid = userid,
                Email = model.Email,
                PhoneNumber = model.phoneNumber,
                EmailVerified = true,
                DisplayName = model.Username,
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);


            var driver = await _ctx.drivers.FirstOrDefaultAsync(x => x.uid == userid);

            driver.Username = model.Username;
            driver.IsWorking = model.IsWorking;
            driver.phoneNumber = model.phoneNumber;
            driver.birthDate = model.birthDate;
            driver.DriverLicenceNumber = model.DriverLicenceNumber;
            driver.Email = model.Email;
            driver.ExpiryDate = model.ExpiryDate;
            driver.Photo = photoPath;
            


            if ( await _ctx.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return BadRequest("Something happend!!");

                

        }


     
      

      
    }
}