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



		// Много преподавателей - много предметов
		public List<Specialty> Specialty { get; set; } = new List<Specialty>();
	}
}
