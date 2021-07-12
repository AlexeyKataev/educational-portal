using System.ComponentModel.DataAnnotations;
using Dotnet.Models;
using Dotnet.Models.Study;

namespace Dotnet.ViewModels.WebApp.Teacher
{
	public class SubjectsForTeacherViewModel 
	{
		[Required]
		public long Id { get; set; }

		public long[] SubjectsId { get; set; }
	}
}