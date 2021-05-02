using System.Collections.Generic;
using Dotnet.Models.Study;

namespace Dotnet.Models
{
	public class Teacher
	{
		public int Id { get; set; }

		public int UserId { get; set;}	

		public string Specialization { get; set; }

		public string Post { get; set; }



		// Один преподавателей - одно задание
		public Work Work { get; set; }

		// Много преподавателей - много предметов
		public List<SubjectTeacher> SubjectTeacher { get; set; } = new List<SubjectTeacher>();
	}
}
