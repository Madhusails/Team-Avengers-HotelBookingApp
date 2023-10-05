using HotelBooking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.API.DAL
{
    public class MyAppDbContext: DbContext
    {
        public MyAppDbContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<RegisteredUsers> RegisteredUsers { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<RoomInfo> RoomInfo { get; set; }
    }
}
