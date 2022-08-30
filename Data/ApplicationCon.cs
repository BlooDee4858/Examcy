using Microsoft.EntityFrameworkCore;

namespace Examcy.Data.Models
{
    public class ApplicationCon  : DbContext
    {
            public DbSet<User> Users { get; set; }
            public DbSet<UserRole> Roles { get; set; }
            public ApplicationCon(DbContextOptions<ApplicationCon> options)
                : base(options)
            {
                Database.EnsureCreated();
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                string adminRoleName = "admin";
                string StudentRoleName = "student";
                string TeacherRoleName = "teacher";

                string adminLogin = "admin";
                string adminPassword = "123456";

                // добавляем роли
               

              
            }
    }
}


