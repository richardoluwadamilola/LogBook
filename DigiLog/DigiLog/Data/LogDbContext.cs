using DigiLog.Models;
using Microsoft.EntityFrameworkCore;
//using MySql.Data;

namespace DigiLog.Data
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions options) : base(options)
        {
        }



        public DbSet<Employee> Employees { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Tag> Tags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                .ToTable("Tags");

            modelBuilder.Entity<Tag>()
                .HasData(
                new Tag
                {
                    TagID = 1,
                    TagNumber = "VIS-A60",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                },
                new Tag
                {
                    TagID = 2,
                    TagNumber = "VIS-A61",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                },
                new Tag
                {
                    TagID = 3,
                    TagNumber = "VIS-A62",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                },
                new Tag
                {
                    TagID = 4,
                    TagNumber = "VIS-A78",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                },
                new Tag
                {
                    TagID = 5,
                    TagNumber = "VIS-A66",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                },
                new Tag
                {
                    TagID = 6,
                    TagNumber = "VIS-A56",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                },
                new Tag
                {
                    TagID = 7,
                    TagNumber = "VIS-A87",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                },
                new Tag
                {
                    TagID = 8,
                    TagNumber = "VIS-A78",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                },
                new Tag
                {
                    TagID = 9,
                    TagNumber = "VIS-A88",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                },
                new Tag
                {
                    TagID = 10,
                    TagNumber = "VIS-A48",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deleted = false
                }
                );
        }

    }
}

