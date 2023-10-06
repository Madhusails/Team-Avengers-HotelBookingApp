using HotelBooking.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Text;

namespace HotelBooking.API.APIServices.cs
{
    public class ApiService:IApiService
    {
        private readonly IHttpClientFactory httpClientFactory;



        public ApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        

        public async Task Register(RegisteredUsers user)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");

            string jsonContent = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/Account/Register", stringContent);
        }

        public async Task Delete(int id)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            await httpClient.DeleteAsync($"api/Account/delete");
        }

        public async Task<List<UserInfo>> Get()
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            var response = await httpClient.GetAsync("api/Account/GetUsers");
            if (response.IsSuccessStatusCode)
            {
                var userList = await response.Content.ReadFromJsonAsync<List<UserInfo>>();
                return userList;
            }
            else
                return null;
        }

        public async Task<UserInfo> GetUserById(int id)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            var response = await httpClient.GetAsync($"api/Booking/Userinfo/{id}");
            var content = await response.Content.ReadAsStringAsync();



            var user = JsonConvert.DeserializeObject<UserInfo>(content);
            return user;
        }

        public async Task Login(RegisteredUsers user)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");

            string jsonContent = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/Account/Login", stringContent);
        }
        

        public async Task AddUserInfo(UserInfoModel user, string roomType)
        {
            try
            {
                
                var httpClient = httpClientFactory.CreateClient("WebAPI");
                var queryParams = new List<KeyValuePair<string, string>>
                 {
                     new KeyValuePair<string, string>("roomType", roomType)
                 };
                string requestUrl = QueryHelpers.AddQueryString("api/Booking/AddUserInfo", queryParams);
                
                

                string jsonContent = JsonConvert.SerializeObject(user);
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(requestUrl, stringContent);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding user info.", ex);
            }
        }



        public async Task<List<RoomInfo>> GetAllRooms(string RoomType, DateTime Checkin, DateTime Checkout)
        {
            var checkIn = Checkin.ToString("yyyy-MM-dd");
            var checkOut = Checkout.ToString("yyyy-MM-dd");
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            string apiUrl = $"api/Booking/GetAvailableRooms/{RoomType}/{checkIn}/{checkOut}";  // Format the URL
            var requestData = new
            {
                RoomType = RoomType,
                Checkin = Checkin,
                Checkout = Checkout
            };
            string jsonContent = JsonConvert.SerializeObject(requestData);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, stringContent);  // Use the formatted URL
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<RoomInfo> roomInfoList = JsonConvert.DeserializeObject<List<RoomInfo>>(content);
                if(roomInfoList.Count<=0)
                {
                 return null;
                }
                else
                {
                    return roomInfoList;
                }
                
            }
            else
            {
                throw new Exception("Failed to retrieve room information");
            }
        }

        
    }
}
