using Microsoft.EntityFrameworkCore;
using SecureApiWithJwt.Models;

namespace SecureApiWithJwt.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Initialize DbSet properties
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Full access to all endpoints" },
                new Role { Id = 2, Name = "Instructor", Description = "Access to instructor-specific endpoints" },
                new Role { Id = 3, Name = "Student", Description = "Access to student-specific endpoints" }
            );

            // Seed initial users
            modelBuilder.Entity<User>().HasData(
                new User { 
                    Id = 1, 
                    FullName = "Alice Green", 
                    Email = "admin@example.com", 
                    PasswordHash = "@dminP4ssw0Rd", 
                    RoleId = 1 
                },
                new User { 
                    Id = 2, 
                    FullName = "Lucas Hallak", 
                    Email = "instructor@example.com", 
                    PasswordHash = "InstrucT0r!", 
                    RoleId = 2 
                },
                new User { 
                    Id = 3, 
                    FullName = "Melina Rosas", 
                    Email = "student@example.com", 
                    PasswordHash = "Stud3ntP@ss", 
                    RoleId = 3 
                }
            );
        }
    }
}
