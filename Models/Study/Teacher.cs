using System.Collections.Generic;
using Dotnet.Models.Study;

namespace Dotnet.Models
{
	public class Teacher
	{
		public int Id { get; set; }

		public int UserId { get; set;}	

		private string specialization;
		public string Specialization 
		{  
			get => specialization ?? "Специальность не указана";
			set { specialization = value; }
		}

		private string post;
		public string Post
		{
			get => post ?? "Должность не указана";
			set { post = value; }
		}



		// Один преподавателей - много заданий
		public List<Work> Works { get; set; } = new List<Work>();

		// Много преподавателей - много предметов
		public List<SubjectTeacher> SubjectTeacher { get; set; } = new List<SubjectTeacher>();
	}
}
