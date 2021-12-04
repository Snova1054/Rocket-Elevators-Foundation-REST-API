using Microsoft.EntityFrameworkCore;

namespace RocketElevatorsFoundationRESTAPI.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
        
        public DbSet<Intervention> interventions { get; set; }
    }
}