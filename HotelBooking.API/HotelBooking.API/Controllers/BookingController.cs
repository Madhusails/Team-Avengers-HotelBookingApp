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
    public class BookingController : ControllerBase
    {
        // to init database
        private readonly IRegistrationRepository _registrationRepository;

        public BookingController(IRegistrationRepository _registrationRepository)
        {
            this._registrationRepository = _registrationRepository;
        }



        [HttpGet("availablerooms/{roomType}/{cIn}/{cOut}")]
        public async Task<IActionResult> GetAvailableRooms(string roomType, DateTime cIn, DateTime cOut)
        {
            var roomInfo = await _registrationRepository.GetAllRooms(roomType, cIn, cOut);
            return Ok(roomInfo);
        }

        [HttpPost("userinfo")]
        public async Task<IActionResult> CreateUserInfo([FromBody] UserInfo userInfoModel, string roomType)
        {
            
            var newUser=await _registrationRepository.AddUserInfo(userInfoModel);
            var updateRoomInfo=await _registrationRepository.UpdateRoomInfo(userInfoModel.No_of_rooms,userInfoModel.Checkin,userInfoModel.Checkout,roomType);
            return Ok(newUser);
        }



    }
}
