using System.ComponentModel.DataAnnotations;

namespace HotelBooking.API.Models
{
    public class UserInfo
    {
        [Key]
        public int UsertId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int No_of_guests { get; set; }
        [Required]
        public int No_of_rooms { get; set; }
        [Required]
        public DateTime Checkin { get; set; }
        [Required]
        public DateTime Checkout { get; set; }
        [Required]
        public int Number_of_days { get; set; }
        

    }
}
