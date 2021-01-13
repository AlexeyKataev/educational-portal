using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Задание"	
	*/

	public class Work
	{
		public int Id { get; set; }

		[Column(TypeName = "text")]
		public string Description { get; set; }

		public bool IsObligation { get; set; }

		public System.DateTime DateAdded { get; set; }

		public System.DateTime DateDeparture { get; set; }

		public int CountAttempts { get; set; }

		

		// Одно задание - один преподаватель
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }

		// Много заданий - один предмет
		public List<Subject> Subjects { get; set; } = new List<Subject>();

		// Много заданий - один предмет
		public List<StudySubgroup> StudySubgroups { get; set; } = new List<StudySubgroup>();

		// Много заданий - один предмет
		public List<TypeWorks> TypesWorks { get; set; } = new List<TypeWorks>();

		// Одно задание - много файлов
		public int FileId { get; set; }
		public File File { get; set; }	
	}
}
