using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VueJSDotnet51.Data;
using VueJSDotnet51.Models;
using VueJSDotnet51.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting.Internal;
using VueJSDotnet51.Data.FirebaseManager;
using System.Globalization;

namespace VueJSDotnet51.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        private readonly ApiContext _ctx;
        private readonly IFileManager _fileManager;
        private readonly IFirebaseManager _firebaseManager;

        public AuthController(ApiContext ctx, IFileManager fileManager, IFirebaseManager firebaseManager)
        {
          
            _ctx = ctx;
           _fileManager = fileManager;
            _firebaseManager = firebaseManager;
        }

        /// <summary>
        /// Register a new driver
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">if the driver creadted successfully</response>
        /// <response code="400">if somthing went worong</response>
        /// <returns></returns>
        [HttpPost("register/driver")]
        public async Task<IActionResult> RegisterDrvier([FromForm] RegisterDriverModel model)
        {
            try
            {
                bool valid = Extensions.IsValidStringDate(model.birthDate);
                var user = _ctx.drivers.Where(x => x.phoneNumber == model.phoneNumber).FirstOrDefault();

                if (user != null)
                {
                    return BadRequest("User Alreday exist!");
                }
                else
                {
                    if (!valid)
                    {
                        return BadRequest("Birthdate is not a valid date, dd/MM/yyyy");
                    }

                    else
                    {

                        //Save File
                        var result = await Extensions.UploadImage(model.Photo);

                        // Register A user 
                        UserRecordArgs args = new UserRecordArgs()
                        {

                            EmailVerified = false,
                            PhoneNumber = model.phoneNumber,
                            Password = model.Password,
                            DisplayName = model.Username,
                        };


                        // Adding Claims
                        var claims = new Dictionary<string, object>()
            {
             { "Role", "driver" },
            };


                        var userRecord = await _firebaseManager.CreateUser(args, claims);

                        if (userRecord != null)
                        {
                            var driver = new Driver();

                            driver.Email = model.Email;
                            driver.uid = userRecord.Uid;
                            driver.Username = model.Username;
                            driver.phoneNumber = model.phoneNumber;
                            driver.Photo = result.Url.ToString();
                            driver.IsWorking = model.IsWorking;
                            driver.DriverLicenceNumber = model.DriverLicenceNumber;
                            driver.birthDate = model.birthDate;
                            driver.ExpiryDate = model.ExpiryDate;

                            _ctx.drivers.Add(driver);
                        }
                        else
                        {
                            return BadRequest("User Not registred in firebase!");
                        }


                        if (await _ctx.SaveChangesAsync() > 0)
                        {
                            return Ok(new { status = "Success", message = "Driver registred successfully" });
                        }

                    }


                    return BadRequest();

                }

             
            }
            catch (FirebaseAuthException ex)
            {

                return BadRequest(ex.Message);
            }

           
            
        }





        [HttpPost("register/rider")]
        public async Task<IActionResult> RegisterRider([FromForm] RegisterRiderModel model)
        {
            try
            {
                
                var user = _ctx.riders.Where(x => x.phoneNumber == model.phoneNumber).FirstOrDefault();
                if (user != null)
                {
                    return BadRequest("User Already exist!");
                }
                else
                {
                    //Save File
                    var result = await Extensions.UploadImage(model.Photo);

                    // Register A user 
                    UserRecordArgs args = new UserRecordArgs()
                    {

                        EmailVerified = false,
                        PhoneNumber = model.phoneNumber,
                        Password = model.Password,
                        DisplayName = model.Username,
                    };


                    // Adding Claims
                    var claims = new Dictionary<string, object>()
            {
             { "Role", "rider" },
            };


                    var userRecord = await _firebaseManager.CreateUser(args, claims);

                    if (userRecord != null)
                    {
                        var rider = new Rider();
                        rider.email = model.Email;
                        rider.uid = userRecord.Uid;
                        rider.phoneNumber = model.phoneNumber;
                        rider.Username = model.Username;
                        rider.Photo = result.Url.ToString();


                        _ctx.riders.Add(rider);
                    }
                    else
                    {
                        return BadRequest("User not registred in firebase!");
                    }


                    if (await _ctx.SaveChangesAsync() > 0)
                    {
                        return Ok(new { status = "Success", message = "Rider registred successfully" });
                    }


                    return BadRequest();
                }

             
            }
            catch(FirebaseAuthException ex)
            {

                return BadRequest(ex.Message);
            }


        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {

            try
            {
                var userexist = _ctx.users.Where(x => x.Email == model.Email).FirstOrDefault();

                if(userexist != null)
                {
                    return BadRequest("User Already exist!");
                }
                else
                {
                    //Save File
                    var result = await Extensions.UploadImage(model.Photo);

                    // Register A user 
                    UserRecordArgs args = new UserRecordArgs()
                    {
                        Email = model.Email,
                        EmailVerified = false,
                        Password = model.Password,
                        DisplayName = model.Username,
                    };

                    // Adding Claims

                    var claims = new Dictionary<string, object>()
            {
             { "Role", model.role },
            };

                    var userRecord = await _firebaseManager.CreateUser(args, claims);

                    if (userRecord != null)
                    {
                        var user = new User();
                        user.Username = model.Username;
                        user.Email = model.Email;
                        user.uid = userRecord.Uid;
                        user.PhoneNumber = model.phoneNumber;
                        user.Photo = result.Url.ToString();

                        _ctx.users.Add(user);

                    }
                    else
                    {
                        return BadRequest("User not registred in firebase!");
                    }

                    if (await _ctx.SaveChangesAsync() > 0)
                    {
                        return Ok(new { status = "Success", message = "User registred successfully" });
                    }

                    return BadRequest();
                }
            }
            catch (FirebaseAuthException ex)
            {

                return BadRequest(ex.Message);
            }
         
        }

        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
           var result = await Extensions.UploadImage(file);
            return Ok(result.Url);
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginModel model)
        //{
        //    //var user = await _usermanager.FindByEmailAsync(model.Email);

        //    //var roles = await _usermanager.GetRolesAsync(user);

        //    //if(user != null && await _usermanager.CheckPasswordAsync(user, model.Password))
        //    //{

        //    //    List<Claim> claims = new List<Claim>();
        //    //    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        //    //    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
        //    //    foreach (var role in roles)
        //    //    {
        //    //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    //    }


        //    //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        //    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        //    //    var tokenDescriptor = new SecurityTokenDescriptor
        //    //    {
        //    //        Subject = new ClaimsIdentity(claims),
        //    //        Expires = System.DateTime.Now.AddHours(3),
        //    //        SigningCredentials = creds
        //    //    };

        //    //    var tokenhandler = new JwtSecurityTokenHandler();
        //    //    var token = tokenhandler.CreateToken(tokenDescriptor);


        //    //    return Ok(new { 
        //    //        token = tokenhandler.WriteToken(token),
        //    //        Expires = token.ValidTo,
        //    //        User = user.UserName
        //    //    });

        //    //}

        //    return Unauthorized();

        //}




        //[HttpPost("login/phone")]
        //public async Task<IActionResult> LoginWithPhoneNumber(PhoneLoginModel model)
        //{
        //    //var user = _ctx.Users.Where(x => x.PhoneNumber == model.phoneNumber).SingleOrDefault();
        //    //var roles = await _usermanager.GetRolesAsync(user);

        //    //if (user != null && await _usermanager.CheckPasswordAsync(user, model.Password))
        //    //{

        //    //    List<Claim> claims = new List<Claim>();
        //    //    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        //    //    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
        //    //    foreach (var role in roles)
        //    //    {
        //    //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    //    }


        //    //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        //    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        //    //    var tokenDescriptor = new SecurityTokenDescriptor
        //    //    {
        //    //        Subject = new ClaimsIdentity(claims),
        //    //        Expires = System.DateTime.Now.AddHours(3),
        //    //        SigningCredentials = creds
        //    //    };

        //    //    var tokenhandler = new JwtSecurityTokenHandler();
        //    //    var token = tokenhandler.CreateToken(tokenDescriptor);


        //    //    return Ok(new
        //    //    {
        //    //        token = tokenhandler.WriteToken(token),
        //    //        Expires = token.ValidTo,
        //    //        User = user.UserName
        //    //    });

        //    //}

        //    return Ok();

        //}




    }
}