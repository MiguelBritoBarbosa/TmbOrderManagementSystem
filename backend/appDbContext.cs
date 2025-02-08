using Microsoft.EntityFrameworkCore;
using TmbOrderManagementSystem.Api.Orders;

namespace TmbOrderManagementSystem.Api
{
    public class appDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? dbPort = Environment.GetEnvironmentVariable("POSTGRES_PORT");
            string? dbName = Environment.GetEnvironmentVariable("POSTGRES_DB");
            string? dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
            string? dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
            string connectionString = $"Host=database;Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";

            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
