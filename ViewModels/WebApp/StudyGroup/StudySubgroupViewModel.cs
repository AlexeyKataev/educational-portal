using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.StudyGroup
{
    public class StudySubgroupViewModel
    {
		[Required(ErrorMessage = "Укажите ID учебной группы")]
		public ulong StudyGroupId { get; set; }

		[Required(ErrorMessage = "Укажите название учебной подгруппы")]
		public string Name { get; set; }

		public string Code { get; set; }
    }
}