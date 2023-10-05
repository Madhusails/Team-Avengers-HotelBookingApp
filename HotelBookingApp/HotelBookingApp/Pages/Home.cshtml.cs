using HotelBooking.API.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelBookingApp.Pages
{
   /* [Authorize]*/
    public class HomeModel : PageModel
    {
        private readonly IRegistrationRepository _registrationRepository;

        public HomeModel(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
        [BindProperty]
        public string RoomType { get; set; }

        [BindProperty]
        public DateTime? Checkin { get; set; }

        [BindProperty]
        public DateTime? Checkout { get; set; }

        public bool ShowTable { get; set; }
        public List<RoomInfo> RoomInfoList { get; set; }
        /*public List<RoomInfo> roomInfo { get; set; }*/
        public void OnGet()
        {
            ShowTable = false;
            
            //ViewData["RoomInfoList"] = roomInfo;


        }
        public async void OnPost()
        {
            TempData["RoomType"] = RoomType;
            RoomInfoList = await _registrationRepository.GetAllRooms(RoomType, Checkin, Checkout);
            /*TempData["roomList"] = RoomInfoList;*/
            if (!string.IsNullOrEmpty(RoomType))
            {
                RoomInfoList = RoomInfoList
                    .Where(p => p.RoomType.Contains(RoomType, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            if (RoomInfoList != null && RoomInfoList.Any())
            {
                ShowTable = true;
            }
            else
            {
                ShowTable = false;
            }
            
        }
    }
}
