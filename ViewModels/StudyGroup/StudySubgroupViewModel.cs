using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.StudyGroup
{
    public class StudySubgroupViewModel
    {
		[Required(ErrorMessage = "Укажите ID учебной группы")]
		public int StudyGroupId { get; set; }

		[Required(ErrorMessage = "Укажите название учебной подгруппы")]
		public string Name { get; set; }

		public string Code { get; set; }
    }
}