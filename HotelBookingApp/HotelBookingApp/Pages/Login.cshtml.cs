using HotelBooking.API.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBookingApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IRegistrationRepository _registrationRepository;
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public LoginModel(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            RegisteredUsers registeredUsers = _registrationRepository.GetUserByUsernameAndPassword(UserName, Password);
            if (registeredUsers != null)
            {

                // Successful login, you can redirect or do something else here
                return RedirectToPage("Home");
            }

            // Failed login, display an error message or handle it as needed
            ModelState.AddModelError("", "Invalid username or password");
            ModelState.Clear();

            return Page();
        }
    }
}
