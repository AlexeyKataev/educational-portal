using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Файл"	
	*/

	public class File
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string PathFile { get; set; }

		public bool Vanish { get; set; }

		public System.DateTime NeedToDelete { get; set; }

		public System.DateTime dateAdded { get; set; }



		// Много файлов - одно задание
		public List<Work> Works { get; set; } = new List<Work>();
	}
}
