using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Учебная подгруппа"	
	*/

	public class StudySubgroup
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }



		// Одна подгруппа - много заданий
		public int WorkId { get; set; }
		public Work Work { get; set; }	

		// Одна подгруппа - одна группа
		public int StudyGroupId { get; set; }
		public StudyGroup StudyGroup { get; set; }
	}
}
