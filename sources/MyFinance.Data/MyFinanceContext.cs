using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MyFinance.Domain;
using MyFinance.Domain.Entities;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.SqlServer;

namespace MyFinance.Data

{
    public class MyFinanceContext : DbContext
    {
        public MyFinanceContext() : base("DefaultConnection") {
            this.Configuration.LazyLoadingEnabled = true;            
            Database.SetInitializer<MyFinanceContext>(new MigrateDatabaseToLatestVersion<MyFinanceContext, MyFinanceContextInitializer>());
        }
       // public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoomClass> RoomClasses { get; set; }
        public DbSet<RoomAttribute> RoomExtend { get; set; }        
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<GroupWidget> GroupWidgets { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
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
        public DbSet<Shift> Shift { get; set; }
        public DbSet<OwnerHotel> OwnerHotel { get; set; }
        public DbSet<Camera> Camera { get; set; }

        // public DbSet<Country> Country { get; set; }
        // public DbSet<Ticket> Ticket { get; set; }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //Database.SetInitializer<MyFinanceContext>(null);
            Database.SetInitializer<MyFinanceContext>(new MigrateDatabaseToLatestVersion<MyFinanceContext, MyFinanceContextInitializer>());
            //modelBuilder.Entity<User>().Property(a=>a.Id).(1);
           


             base.OnModelCreating(modelBuilder);

             //modelBuilder.Entity<User>().ToTable("Users");
             //modelBuilder.Entity<Category>().ToTable("Category");
             //modelBuilder.Entity<Hotel>().ToTable("Hotels");
        }
    }

    public class MyFinanceContextInitializer : System.Data.Entity.Migrations.DbMigrationsConfiguration<MyFinanceContext>
    {
        public MyFinanceContextInitializer()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MyFinance.Data.MyFinanceContext";
        }

        protected override void Seed(MyFinanceContext context)
        {
            
           // base.Seed(context);
            //var roles = new List<Role>{
            //    new Role{RoleName = "Administrator"},
            //    new Role{RoleName = "User"}               
            //};

            //roles.ForEach(r => context.Roles.Add(r));
        }
    }
}
