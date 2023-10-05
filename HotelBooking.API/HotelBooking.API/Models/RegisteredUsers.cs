using System.ComponentModel.DataAnnotations;

namespace HotelBooking.API.Models
{
    public class RegisteredUsers
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
