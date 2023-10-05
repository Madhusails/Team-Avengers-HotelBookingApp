using Dapper;
using HotelBooking.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HotelBookingApp.Repositories
{
    public class SQLRegistrationRepository : IRegistrationRepository
    {
        private IDbConnection db;
        public SQLRegistrationRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("HotelBookingConnectionString"));
        }

        
        public async Task<RegisteredUsers> Create(RegisteredUsers registeredUsers)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", registeredUsers.Email);
            parameters.Add("@UserName", registeredUsers.UserName);
            parameters.Add("@Password", registeredUsers.Password);

            var id = await db.QuerySingleAsync<int>(
                "_uspAddRegisteredUser",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            registeredUsers.Id = id;
            return registeredUsers;
            
        }

        public async Task<UserInfo> AddUserInfo(UserInfo userInfo)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", userInfo.Name);
            parameters.Add("@Age", userInfo.Age);
            parameters.Add("@No_of_guests", userInfo.No_of_guests);
            parameters.Add("@No_of_rooms", userInfo.No_of_rooms);
            parameters.Add("@Checkin", userInfo.Checkin);
            parameters.Add("@Checkout", userInfo.Checkout);
           
            var id = await db.QuerySingleAsync<int>(
               "_uspAddUserInfo",
               parameters,
               commandType: CommandType.StoredProcedure
           );

            userInfo.UsertId = id;
            return userInfo;

            
        }
        public RegisteredUsers GetUserByUsernameAndPassword(string username, string password)
        {

           
            return db.QueryFirstOrDefault<RegisteredUsers>("_uspGetUserByUsernameAndPassword", new { Username = username, Password = password }, commandType: CommandType.StoredProcedure);

        }

       

        
        public async Task<int> UpdateRoomInfo(int numberOfRooms, DateTime checkin, DateTime Checkout, string RoomType)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@NumberOfRooms", numberOfRooms);
            parameters.Add("@Checkin", checkin);
            parameters.Add("@Checkout", Checkout);
            parameters.Add("@RoomType", RoomType);

            return await db.ExecuteAsync("_uspUpdateRoomInfo", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<RoomInfo>> GetAllRooms(string RoomType, DateTime? Checkin, DateTime? Checkout)
        {
            var sql = "SELECT * FROM RoomInfo WHERE RoomType=@RoomType AND isbooked=0 AND Checkin<=@Checkin AND Checkout<@Checkout and @Checkin <= @Checkout";
            var roomInfoList = await db.QueryAsync<RoomInfo>(sql, new { RoomType, Checkin, Checkout });
            return roomInfoList.ToList();
        }

        public void DeleteUser(string username)
        {
            db.Execute("_uspDeleteUserByUsername", new { username }, commandType: CommandType.StoredProcedure);
        }

        
    }
}
