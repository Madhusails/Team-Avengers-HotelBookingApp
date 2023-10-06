using HotelBooking.API.APIServices.cs;
using HotelBooking.API.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBookingApp.Pages
{
    public class PersonalInfoModel : PageModel
    {
        private readonly IApiService _apiService;
        public string roomType { get; set; }
        public PersonalInfoModel(IApiService _apiService)
        {
            this._apiService = _apiService;
        }

        
        public void OnGet()
        {
            if (TempData.ContainsKey("RoomType"))
                 roomType = TempData["RoomType"].ToString();
            TempData.Keep("RoomType");
        }
        public async Task<IActionResult> OnPost(UserInfoModel user)
        {
            await _apiService.AddUserInfo(user, TempData["RoomType"].ToString());
            ModelState.Clear();

            return RedirectToPage("Success");

        }
    }
}
