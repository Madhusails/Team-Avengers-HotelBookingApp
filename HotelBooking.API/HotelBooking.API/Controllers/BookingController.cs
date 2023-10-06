using HotelBooking.API.DAL;
using HotelBooking.API.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HotelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        // to init database
        private readonly IRegistrationRepository _registrationRepository;

        public BookingController(IRegistrationRepository _registrationRepository)
        {
            this._registrationRepository = _registrationRepository;
        }



        [HttpPost("GetAvailableRooms/{roomType}/{cIn}/{cOut}")]
        public async Task<IActionResult> GetAvailableRooms(string roomType, DateTime cIn, DateTime cOut)
        {
            var roomInfo = await _registrationRepository.GetAllRooms(roomType, cIn, cOut);
            //return Ok(roomInfo);
            if (roomInfo.Count <= 0)
            {
                return NotFound($"Sorry {roomType} rooms are booked for that date range");
            }
            else
            {
                return Ok(roomInfo);
            }
        }

        [HttpPost("AddUserInfo")]
        public async Task<IActionResult> AddUserInfo([FromBody] UserInfo userInfoModel, string roomType)
        {
            var getRooms = await _registrationRepository.GetAllRooms(roomType, userInfoModel.Checkin, userInfoModel.Checkout);
            // room type validation
            if (!string.Equals(roomType, "AC", StringComparison.OrdinalIgnoreCase) && !string.Equals(roomType, "Non-AC", StringComparison.OrdinalIgnoreCase) && !string.Equals(roomType, "Suit Room", StringComparison.OrdinalIgnoreCase) && !string.Equals(roomType, "AC Deluxe", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid room type.");
            }

            if (getRooms.Count >= userInfoModel.No_of_rooms)
            {
                // Validate Name (should only contain alphabets)
                if (!Regex.IsMatch(userInfoModel.Name, @"^[a-zA-Z]+$"))
                {
                    return BadRequest("Name should only contain alphabets.");
                }

                // Validate Age (should be between 1 and 100)
                else if (userInfoModel.Age < 1 || userInfoModel.Age > 100)
                {
                    return BadRequest("Age should be between 1 and 100 years.");
                }
                else
                {
                    userInfoModel.Number_of_days = (userInfoModel.Checkout - userInfoModel.Checkin).Days;
                    var newUser = await _registrationRepository.AddUserInfo(userInfoModel);
                    var updateRoomInfo = await _registrationRepository.UpdateRoomInfo(userInfoModel.No_of_rooms, userInfoModel.Checkin, userInfoModel.Checkout, roomType);
                    return Ok(newUser);
                }
            }
            else
            {
                return BadRequest($"Available rooms are {getRooms.Count} and you choose to book {userInfoModel.No_of_rooms} rooms");
            }
            
        }
    }
}
