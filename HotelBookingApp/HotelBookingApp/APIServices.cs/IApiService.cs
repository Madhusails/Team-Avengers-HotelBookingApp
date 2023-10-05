using HotelBooking.API.Models;

namespace HotelBooking.API.APIServices.cs
{
    public interface IApiService
    {
        Task<List<UserInfo>> Get();
        Task AddUser(UserInfo user);
        Task<UserInfo> GetUserById(int id);

        Task AddPersonalInfo(UserInfo user);
        Task Delete(int id);
    }
}
