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
            DateTime checkin = (DateTime)TempData["Checkin"];
            DateTime checkout = (DateTime)TempData["Checkout"];
            if (checkin <= user.Checkin && checkout >= user.Checkout && checkin <= checkout && user.Checkin <= user.Checkout)
            {
                await _apiService.AddUserInfo(user, TempData["RoomType"].ToString());
                ModelState.Clear();
                return RedirectToPage("Success");
            }
            else
            {
                TempData["Message"] = "Booking dates not matched with available dates";
                return RedirectToPage("Failure");
            }

           

        }
    }
}
