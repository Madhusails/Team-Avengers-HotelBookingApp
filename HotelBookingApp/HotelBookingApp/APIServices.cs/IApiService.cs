using HotelBooking.API.Models;

namespace HotelBooking.API.APIServices.cs
{
    public interface IApiService
    {
        Task<List<UserInfo>> Get();
        Task Register(RegisteredUsers user);
        Task Login(RegisteredUsers user);
        Task<List<RoomInfo>> GetAllRooms(string RoomType, DateTime Checkin, DateTime Checkout);
        Task<UserInfo> GetUserById(int id);

        Task AddUserInfo(UserInfoModel user, string roomType);
        //Task UpdateRoomInfo(UserInfo room);
        Task Delete(int id);

       
    }
}
