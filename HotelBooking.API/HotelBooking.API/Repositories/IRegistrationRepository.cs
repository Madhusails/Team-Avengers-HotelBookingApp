using HotelBooking.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Repositories
{
    public interface IRegistrationRepository
    {
        Task<RegisteredUsers> Create(RegisteredUsers registeredUsers);
        Task<UserInfo> AddUserInfo(UserInfo userInfo);

        Task<int> UpdateRoomInfo(int numberOfRooms,DateTime checkin,DateTime Checkout,string RoomType);
        public RegisteredUsers GetUserByUsernameAndPassword(string username, string password);
        Task<List<RoomInfo>> GetAllRooms(string RoomType,DateTime? Checkin,DateTime? Checkout);
        
        public void DeleteUser(string username);
        //Task<IEnumerable<RoomInfo>> GetAllRooms();
    }
}
