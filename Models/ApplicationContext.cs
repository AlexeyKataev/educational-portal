using Microsoft.EntityFrameworkCore;
 
namespace Dotnet.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Объявляем роли для учётных записей
            string adminRoleName = "admin";
            string userRoleName = "user";
 
			// Устанавливаем параметры по умолчанию для учётной записи администратора
            string adminLogin = "admin";
            string adminPassword = "123";
 
            // Создаём объекты ролей
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };

			// Создаём объект учётной записи администратора
            User adminUser = new User { Id = 1, Login = adminLogin, Password = adminPassword, RoleId = adminRole.Id };
 
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}