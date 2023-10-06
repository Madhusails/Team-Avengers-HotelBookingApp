using HotelBooking.API.APIServices.cs;
using HotelBooking.API.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBookingApp.Pages
{
    public class LoginModel : PageModel
    {
        
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }
        private readonly IApiService _apiService;
        public LoginModel(IApiService _apiService)
        {
            this._apiService = _apiService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(RegisteredUsers user)
        {
            await _apiService.Login(user);
            ModelState.Clear();

            return RedirectToPage("Home");
        }
    }
}
