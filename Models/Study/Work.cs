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
	}
}
