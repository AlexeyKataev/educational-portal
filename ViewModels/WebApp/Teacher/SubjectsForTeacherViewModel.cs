using System.ComponentModel.DataAnnotations;
using Dotnet.Models;
using Dotnet.Models.Study;

namespace Dotnet.ViewModels.WebApp.Teacher
{
	public class SubjectsForTeacherViewModel 
	{
		[Required]
		public ulong Id { get; set; }

		public ulong[] SubjectsId { get; set; }
	}
}