using System.ComponentModel.DataAnnotations;

namespace HotelBooking.API.Models
{
    public class UserInfoModel
    {
        
        public int UsertId { get; set; }
        
        public string Name { get; set; }
        
        public int Age { get; set; }
        
        public int No_of_guests { get; set; }
        
        public int No_of_rooms { get; set; }
       
        public DateTime Checkin { get; set; }
       
        public DateTime Checkout { get; set; }
       
        public int Number_of_days { get; set; }
        

    }
}
