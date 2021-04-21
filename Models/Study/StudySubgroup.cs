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



		// Одна подгруппа - одна группа
		public int StudyGroupId { get; set; }
		public StudyGroup StudyGroup { get; set; }

		// Много подгрупп - много заданий
		public List<Work> Works { get; set; } = new List<Work>();
	}
}
