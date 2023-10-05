using HotelBooking.API.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBookingApp.Pages
{
    public class PersonalInfoModel : PageModel
    {
        private readonly IRegistrationRepository _registrationRepository;
        public PersonalInfoModel(IRegistrationRepository _registrationRepository)
        {
            this._registrationRepository = _registrationRepository;
        }

        [BindProperty]
        public int No_of_rooms { get; set; }

        [BindProperty]
        public DateTime Checkin { get; set; }

        [BindProperty]
        public DateTime Checkout { get; set; }
        [BindProperty]
        public string RoomType { get; set; }
        public void OnGet()
        {
           
            
        }
        public async Task<IActionResult> OnPost(UserInfo userInfo)
        {
            try
            {
                await _registrationRepository.AddUserInfo(userInfo);
                ModelState.Clear();

                await _registrationRepository.UpdateRoomInfo(No_of_rooms, Checkin, Checkout, RoomType);
                return RedirectToPage("Success");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToPage("Failure");
            }

        }
    }
}
