
// Data/AppDbContext.cs

using Demo_ASP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo_ASP_API.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
    }
}