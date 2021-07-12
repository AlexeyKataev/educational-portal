using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.StudySubject
{
    public class EditStudySubjectViewModel
    {
		[Required]
		public long Id { get; set; }

		[Required(ErrorMessage = "Укажите название учебного предмета")]
		public string Name { get; set; }
    }
}