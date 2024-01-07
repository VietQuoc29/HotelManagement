using HotelManagerFull.Share.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManagerFull.Share.Connection
{
    /// <summary>
    /// DatabaseContext
    /// </summary>
    public class DatabaseContext : IdentityDbContext
    {
        /// <summary>
        /// DatabaseContext
        /// </summary>
        /// <param name="options"></param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Floors> Floors { get; set; }
        public virtual DbSet<HotelImages> HotelImages { get; set; }
        public virtual DbSet<Hotels> Hotels { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual DbSet<Roles> Roles { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        public virtual DbSet<RoomCategories> RoomCategories { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<RoomStatus> RoomStatus { get; set; }
        public virtual DbSet<ServiceCategories> ServiceCategories { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Sex> Sex { get; set; }
        public virtual DbSet<UserProfiles> UserProfiles { get; set; }
        public virtual DbSet<OrderRoom> OrderRoom { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<RegisterRooms> RegisterRooms { get; set; }

    }
}
