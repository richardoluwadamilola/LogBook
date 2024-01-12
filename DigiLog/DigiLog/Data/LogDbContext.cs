using DigiLog.Models;
using Microsoft.EntityFrameworkCore;
//using MySql.Data;

namespace DigiLog.Data
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }



        public DbSet<Employee> Employees { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visitor>()
                .Property(v => v.ReasonForVisit)
                .HasConversion<string>();
        }
        

    }
}

