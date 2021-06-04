using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.StudySubject
{
    public class EditStudySubjectViewModel
    {
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите название учебного предмета")]
		public string Name { get; set; }
    }
}