using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Тип заданий"	
	*/

	public class TypeWorks
	{
		public int Id { get; set; }

		public string Name { get; set; }



		// Один тип работ - много заданий
		public List<Work> Works { get; set; } = new List<Work>();
	}
}
