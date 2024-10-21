using Microsoft.EntityFrameworkCore;
using Registration.Models;

namespace Registration.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Candidates> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Science", DisplayOrder = 1 },
                new Course { Id = 2, Name = "Commerce", DisplayOrder = 2 },
                new Course { Id = 3, Name = "Arts", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Candidates>().HasData(
               new Candidates { Id = 1 , Name="Den" , Address = "Hubballi" , Class = "1st Puc"},
               new Candidates { Id = 2, Name = "Ben", Address = "Hubballi", Class = "1st Puc" },
               new Candidates { Id = 3, Name = "Glen", Address = "Hubballi", Class = "1st Puc" },
               new Candidates { Id = 4, Name = "Dexter", Address = "Hubballi", Class = "1st Puc" }


                );
        }
    }
}