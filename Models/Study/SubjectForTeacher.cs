using System.Collections.Generic;
using Dotnet.Models.Study;

namespace Dotnet.Models.Study
{
	/* 
	Создание объекта "Учебный предмет, который ведёт преподаватель"	
	*/

	public class SubjectForTeacher : Subject
	{
		public bool IsChecked { get; set; }
	}
}
