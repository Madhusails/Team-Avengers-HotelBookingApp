using HotelBooking.API.APIServices.cs;
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
        private readonly IApiService _apiService;
        public HomeModel(IApiService _apiService)
        {
            this._apiService = _apiService;
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
        public async Task OnPost(RoomInfo roomInfo)
        {
            TempData["RoomType"] = roomInfo.RoomType; // Assuming RoomType is a property of roomInfo
            TempData.Keep("RoomType");

            TempData["CheckIn"] = roomInfo.Checkin; 
            TempData.Keep("CheckIn");
            
            TempData["CheckOut"] = roomInfo.Checkout; 
            TempData.Keep("CheckOut");
            RoomInfoList = await _apiService.GetAllRooms(roomInfo.RoomType, roomInfo.Checkin, roomInfo.Checkout);
            
            if (!string.IsNullOrEmpty(RoomType))
            {
                RoomInfoList = RoomInfoList
                    .Where(p => p.RoomType.Contains(RoomType, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

    }
}
