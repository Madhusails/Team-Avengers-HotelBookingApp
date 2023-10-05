using HotelBooking.API.DAL;
using HotelBooking.API.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRegistrationRepository _registrationRepository;

        public AccountController(IRegistrationRepository _registrationRepository)
        {
            this._registrationRepository = _registrationRepository;
        }

        // get users
        [HttpGet("GetUsers/{username}/{password}")]
        public IActionResult Get(string username,string password)
        {
            var users = _registrationRepository.GetUserByUsernameAndPassword(username,password);
            return Ok(users);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisteredUsers model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _registrationRepository.Create(model);
                if (existingUser == null)
                {
                    return BadRequest("User already exists");
                }
                return Ok("User registered successfully");
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _registrationRepository.GetUserByUsernameAndPassword(model.UserName, model.Password);

                if (user == null)
                {
                    return Unauthorized("Invalid credentials");
                }

                if (user.Password == model.Password)
                {
                    // Authentication successful
                    return Ok("Login successful");
                }
            }

            return Unauthorized("Invalid credentials");
        }
        [HttpDelete("delete")]
        public IActionResult DeleteUser([FromBody] UserDeleteModel model)
        {
            if (ModelState.IsValid)
            {
                // Authenticate the user based on the provided credentials
                _registrationRepository.DeleteUser(model.UserName);

                
                    return Ok("User deleted successfully");
                
            }

            return Unauthorized("Invalid credentials");
        }

    }
}
