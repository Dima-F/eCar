using System.Data.Entity;
using eCar.Applicaton.Models.Configuration.Fluent_API;
using eCar.Applicaton.Models.Service.Entities;
namespace eCar.Applicaton.Models
{
    public class ECarContext:DbContext
    {
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        //Fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AutoConfiguration());
        }
    }
}