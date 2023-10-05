using HotelBooking.API.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBookingApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IRegistrationRepository _registrationRepository;
        public RegisterModel(IRegistrationRepository _registrationRepository)
        {
            this._registrationRepository = _registrationRepository;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(RegisteredUsers user)
        {
            await _registrationRepository.Create(user);
            ModelState.Clear();

            return RedirectToPage("Login");

        }
    }
}
