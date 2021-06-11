using Microsoft.EntityFrameworkCore;
using Dotnet.Models.Study;
using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }



		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<File> Files { get; set; }
		public DbSet<FormEducation> FormsEducation { get; set; }
		public DbSet<Institution> Institutions { get; set; }
		public DbSet<Specialty> Specialties { get; set; }
		public DbSet<StudyGroup> StudyGroups { get; set; }
		public DbSet<StudySubgroup> StudySubgroups { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<TypeWorks> TypesWorks { get; set; }
		public DbSet<Work> Works { get; set; }
		public DbSet<Student> Students { get; set; }

		/* Many-to-many */
		public DbSet<SubjectTeacher> SubjectTeacher { get; set; }
		public DbSet<StudySubgroupWork> StudySubgroupWork { get; set; }
		public DbSet<FileWork> FileWork { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            // Создаём объекты ролей

			/*
			* Администратор
			*
			* ПРАВА:
			*
			* - Полные права доступа.
			*/
			
			string adminLogin = "admin";						// Логин по умолчанию от учётной записи администратора
            string adminPassword = "123";						// Пароль по умолчанию от учётной записи администратора

            string adminFirstName = "Укажите имя";						
            string adminSecondName = "Укажите фамилию";					
            string adminEmail = "Укажите E-Mail";					

            string adminRoleName = "admin";						// Название роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };



			/*
			* Системный администратор
			*
			* ПРАВА:
			* -
			*/

            string systemAdminRoleName = "systemAdmin";			// Название роли
            Role systemAdminRole = new Role { Id = 2, Name = systemAdminRoleName };



			/*
			* Специалист из отдела кадров
			*
			* ПРАВА:
			* -
			*/

            string humanResourcesRoleName = "humanResources";	// Название роли
            Role humanResourcesRole = new Role { Id = 3, Name = humanResourcesRoleName };



			/*
			* Специалист из учебного отдела
			*
			* ПРАВА:
			* -
			*/

            string trainingDivisionRoleName = "trainingDivision";// Название роли
            Role trainingDivisionRole = new Role { Id = 4, Name = trainingDivisionRoleName };



			/*
			* Автор информационных рассылок и создатеть новостных статей
			*
			* ПРАВА:
			* -
			*/

            string authorArticlesRoleName = "authorArticles";	// Название роли
            Role authorArticlesRole = new Role { Id = 5, Name = authorArticlesRoleName };



			/*
			* Преподаватель
			*
			* ПРАВА:
			*
			* - Просмотр списков студентов, преподавателем которых он назначен в данный момент либо был назначен в прошлом.
			* - Создание, редактирование заданий и пересдач в рамках контрольных точек.
			* - Выставление оценок на основе завершённых заданий.
			* - Информационная рассылка в рамках своих учебных групп. 
			* - Рассылка электронных учебных материалов.
			* - Просмотр расписания студентов.
			*/

            string teacherRoleName = "teacher";					// Название роли
			Role teacherRole = new Role { Id = 6, Name = teacherRoleName };



			/*
			* Абитуриент
			*
			* ПРАВА:
			* -
			*/

            string enrolleRoleName = "enrolle";					// Название роли
            Role enrolleRole = new Role { Id = 7, Name = enrolleRoleName };



			/*
			* Выпускник
			*
			* ПРАВА:
			* -
			*/

            string graduateRoleName = "graduate";				// Название роли
            Role graduateRole = new Role { Id = 8, Name = graduateRoleName };



			/*
			* Студент
			*
			* ПРАВА:
			* -
			*/

            string studentRoleName = "student";					// Название роли
            Role studentRole = new Role { Id = 9, Name = studentRoleName };



			/*
			* Пользователь
			*
			* ПРАВА:
			* -
			*/

            string userRoleName = "user";						// Название роли
            Role userRole = new Role { Id = 10, Name = userRoleName };



			// Создаём объект учётной записи администратора
            User adminUser = new User { 
				Id 			= 1, 
				Login		= adminLogin, 
				FirstName 	= adminFirstName,
				SecondName	= adminSecondName,
				DateAdded	= new System.DateTime(0001, 01, 01, 01, 01, 01),
				Email 		= adminEmail,
				Password 	= adminPassword, 
				RoleId 		= adminRole.Id 
			};
 
            modelBuilder.Entity<Role>().HasData(new Role[] { 
				adminRole,
				systemAdminRole,
				humanResourcesRole,
				trainingDivisionRole,
				authorArticlesRole,
				teacherRole,
				enrolleRole,
				graduateRole,
				studentRole,
				userRole 
			});
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}