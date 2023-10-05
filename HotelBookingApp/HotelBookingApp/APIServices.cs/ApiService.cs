using HotelBooking.API.Models;
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

        public async Task AddPersonalInfo(UserInfo user)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");

            string jsonContent = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/Booking/Userinfo", stringContent);
        }

        public async Task AddUser(UserInfo user)
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
    }
}
