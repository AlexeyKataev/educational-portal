using System.ComponentModel.DataAnnotations;
using Dotnet.Models;
using Dotnet.Models.Study;

namespace Dotnet.ViewModels.Teacher
{
	public class SubjectsForTeacherViewModel 
	{
		[Required]
		public int Id { get; set; }

		public int[] SubjectsId { get; set; }
	}
}