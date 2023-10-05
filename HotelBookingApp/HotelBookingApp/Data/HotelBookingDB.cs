using HotelBooking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Data
{
    public class HotelBookingDB:DbContext
    {
        public HotelBookingDB(DbContextOptions options) : base(options) { }
        public DbSet<RegisteredUsers> RegisteredUsers { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<RoomInfo> RoomInfo { get; set; }
       
    }
}
