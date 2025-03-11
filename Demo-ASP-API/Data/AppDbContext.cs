
// Data/AppDbContext.cs

using Demo_ASP_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Demo_ASP_API.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ILogger _logger;
        public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger) : base(options)
        {
            _logger = logger;
            this.Database.EnsureCreated();
            var databaseCreator = this.GetService<IRelationalDatabaseCreator>();

            try
            {
                databaseCreator.CreateTables();

            }catch (Exception ex)
            {
                _logger.LogError("Could not create tables {s}", ex.Message);
            }
           
        }

        public DbSet<Application> Applications { get; set; }
    }
}