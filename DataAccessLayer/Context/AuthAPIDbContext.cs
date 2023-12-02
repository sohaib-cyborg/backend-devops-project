using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using DataAccessLayer.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccessLayer.Context
{
    public class AuthAPIDbContext : IdentityDbContext<ApplicationUser>
    {

        public AuthAPIDbContext()
        {

        }
        public AuthAPIDbContext(DbContextOptions<AuthAPIDbContext> options):base(options)
        {
   
        }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Tracking> Tracking { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sql-server-container;Database=AuthAPIStore;User Id=SA;Password=Msh@hzaib123;MultipleActiveResultSets=true;TrustServerCertificate=True", b => b.MigrationsAssembly("WebAPITask"));
        }

    }
}
