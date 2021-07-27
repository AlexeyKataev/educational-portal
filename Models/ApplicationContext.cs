using Microsoft.EntityFrameworkCore;
using Dotnet.Models.Study;
using Dotnet.Models.Account;
using Dotnet.Models.Messenger;
using Dotnet.Models.Messenger.Chat;
 
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
		public DbSet<UserTicket> UserTickets { get; set; }
		public DbSet<SubjectTeacher> SubjectTeacher { get; set; }
		public DbSet<StudySubgroupWork> StudySubgroupWork { get; set; }
		public DbSet<FileWork> FileWork { get; set; }
		public DbSet<ChatRoom> ChatRooms { get; set; }
		public DbSet<MessageAttachedFile> MessageAttachedFile { get; set; }
		public DbSet<UserChatRoom> UserChatRoom { get; set; }
		public DbSet<UserMessage> UserMessages { get; set; }
		public DbSet<UserMessageChatRoom> UserMessageChatRoom { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
			string adminLogin = "admin";						
            string adminPassword = "123";						

            string adminFirstName = "Укажите имя";						
            string adminSecondName = "Укажите фамилию";					
            string adminEmail = "Укажите E-Mail";					



            string adminRoleName = "admin";						
            Role adminRole = new Role { Id = 1, Name = adminRoleName };

            string systemAdminRoleName = "systemAdmin";			
            Role systemAdminRole = new Role { Id = 2, Name = systemAdminRoleName };

            string humanResourcesRoleName = "humanResources";	
            Role humanResourcesRole = new Role { Id = 3, Name = humanResourcesRoleName};

            string trainingDivisionRoleName = "trainingDivision";
            Role trainingDivisionRole = new Role { Id = 4, Name = trainingDivisionRoleName };

            string authorArticlesRoleName = "authorArticles";	
            Role authorArticlesRole = new Role { Id = 5, Name = authorArticlesRoleName };

            string teacherRoleName = "teacher";					
			Role teacherRole = new Role { Id = 6, Name = teacherRoleName };

            string enrolleRoleName = "enrolle";					
            Role enrolleRole = new Role { Id = 7, Name = enrolleRoleName };

            string graduateRoleName = "graduate";			
            Role graduateRole = new Role { Id = 8, Name = graduateRoleName };

            string studentRoleName = "student";				
            Role studentRole = new Role { Id = 9, Name = studentRoleName };

            string userRoleName = "user";					
            Role userRole = new Role { Id = 10, Name = userRoleName };

            User adminUser = new User { 
				Id 			= 1, 
				Login		= adminLogin, 
				FirstName 	= adminFirstName,
				SecondName	= adminSecondName,
				DateAdded	= new System.DateTime(0001, 01, 01, 01, 01, 01),
				Email 		= adminEmail,
				Password 	= adminPassword, 
				RoleId 		= adminRole.Id,
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
				userRole,
			});
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}