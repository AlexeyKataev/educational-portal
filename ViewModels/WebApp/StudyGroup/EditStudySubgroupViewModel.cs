using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.StudyGroup
{
    public class EditStudySubgroupViewModel
    {
		[Required]
		public ulong Id { get; set; }

		[Required(ErrorMessage = "Укажите ID учебной группы")]
		public ulong StudyGroupId { get; set; }

		[Required(ErrorMessage = "Укажите название учебной подгруппы")]
		public string Name { get; set; }

		public string Code { get; set; }
    }
}