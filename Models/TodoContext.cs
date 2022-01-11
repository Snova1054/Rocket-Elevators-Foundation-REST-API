using Microsoft.EntityFrameworkCore;

namespace RocketElevatorsRESTAPI.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<Building> buildings { get; set; }
        public DbSet<Battery> batteries { get; set; }
        public DbSet<Elevator> elevators { get; set; }
        public DbSet<Column> columns { get; set; }
        public DbSet<Lead> leads { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Intervention> interventions { get; set; }
        public DbSet<Quote> quotes { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<User> users { get; set; }
    }
}