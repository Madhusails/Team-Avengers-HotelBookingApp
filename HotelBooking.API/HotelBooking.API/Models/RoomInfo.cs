using System.ComponentModel.DataAnnotations;

namespace HotelBooking.API.Models
{
    public class RoomInfo
    {
        [Key]
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public bool IsBooked { get; set; }
    }
}
