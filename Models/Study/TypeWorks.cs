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



		// Одна подгруппа - много заданий
		public int WorkId { get; set; }
		public Work Work { get; set; }	
	}
}
