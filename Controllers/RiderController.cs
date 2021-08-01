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
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RiderController : ControllerBase
    {
        private readonly ApiContext _ctx;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;

        public RiderController(ApiContext ctx, IFileManager fileManager, IMapper mapper)
        {
            _ctx = ctx;
           _fileManager = fileManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Riders()
        {
            var res = await _ctx.riders.ToListAsync();
            return Ok(res);
        }


        [HttpGet]
        public IActionResult GetRiderById(int id)
        {
            var res = _ctx.riders.Find(id);
            return Ok(res);
        }


        [HttpGet("{uid}")]
        public async Task<IActionResult> GetRiderByuid(string uid)
        {
            var rider = await _ctx.riders.FirstOrDefaultAsync(x => x.uid == uid);
            return Ok(rider);
        }


        [HttpGet("currentrider")]
        public async Task<IActionResult> GetCurrentRider()
        {

            var uid = User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            var rider = await _ctx.riders.FirstOrDefaultAsync(x => x.uid == uid);
            var res = _mapper.Map<RiderDTO>(rider);
            return Ok(rider);
        }

        [Authorize(Roles = "Rider")]
        [HttpPut]
        public async Task<IActionResult> UpdateRider([FromForm] RiderModel model)
        {

            var photopath = _fileManager.SaveFile(model.Photo);

            var userid = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            UserRecordArgs args = new UserRecordArgs()
            {
                Uid = userid,
                PhoneNumber = model.phoneNumber,
                EmailVerified = true,
                DisplayName = model.username,
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);



            var rider = _ctx.riders.Find(model.Id);

            if (rider != null)
            {
                rider.Photo = photopath;
                rider.phoneNumber = model.phoneNumber;
                rider.Username = model.username;
                rider.email = model.Email;
            }

            if (await _ctx.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest();

        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRider(int id)
        {
            var rider = _ctx.riders.Find(id);

            _ctx.riders.Remove(rider);
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(rider.uid);

            if (await _ctx.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return BadRequest();
        

        }

    }
}