using HotelBooking.API.APIServices.cs;
using HotelBooking.API.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBookingApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IApiService _apiService;
        public RegisterModel(IApiService _apiService)
        {
            this._apiService = _apiService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(RegisteredUsers user)
        {
            await _apiService.Register(user);
            ModelState.Clear();

            return RedirectToPage("Login");

        }
    }
}
