using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Преподаватель"	
	*/

	public class Teacher
	{
		public int Id { get; set; }

		public int UserId { get; set;}	

		public string Specialization { get; set; }

		public string Post { get; set; }



		// Один преподавателей - одно задание
		public Work Work { get; set; }

		// Много преподавателей - много предметов
		public List<Subject> Subjects { get; set; } = new List<Subject>();
	}
}
