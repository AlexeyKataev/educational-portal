using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.StudySubject
{
    public class StudySubjectViewModel
    {
		[Required(ErrorMessage = "Укажите название учебного предмета")]
		public string Name { get; set; }
    }
}