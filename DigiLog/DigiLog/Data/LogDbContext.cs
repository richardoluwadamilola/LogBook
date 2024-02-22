using DigiLog.Models;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReasonForVisit> ReasonForVisit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed ReasonForVisit into the database.
            modelBuilder.Entity<ReasonForVisit>().HasData(
                new ReasonForVisit { Id = 1, Reason = "Official" },
                new ReasonForVisit { Id = 2, Reason = "Personal" },
                new ReasonForVisit { Id = 3, Reason = "Interview" },
                new ReasonForVisit { Id = 4, Reason = "Delivery" }
            );

        }





    }
}


