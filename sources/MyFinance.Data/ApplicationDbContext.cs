using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyFinance.Domain;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<MyFinanceContext>(new MigrateDatabaseToLatestVersion<MyFinanceContext, MyFinanceContextInitializer>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RoomClass> RoomClasses { get; set; }
        public DbSet<RoomAttribute> RoomExtend { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<GroupWidget> GroupWidgets { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCustomer> OrderCustomer { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderService> OrderService { get; set; }
        public DbSet<HotelConfig> HotelConfig { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public DbSet<Utility> RoomTypeFeatures { get; set; }
        public DbSet<UtilityMapping> RoomTypeMappingFeatures { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<InsideUser> InsideUser { get; set; }
        public DbSet<ConfigPrice> ConfigPrice { get; set; }
        public DbSet<UtilityGroup> RoomTypeFeaturesGroup { get; set; }
        public DbSet<Token> Token { get; set; }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
