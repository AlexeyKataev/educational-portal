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
		public int SubjectId { get; set; }
		public Subject Subject { get; set; }

		// Много заданий - один одна группа
		public int StudyGroupId { get; set; }
		public StudyGroup StudyGroup { get; set; }

		// Один тип работы - много заданий
		public int TypeWorksId { get; set; }
		public TypeWorks TypeWorks { get; set; }

		// Много заданий - много файлов
		public List<File> Files { get; set; } = new List<File>();
	
	}
}
