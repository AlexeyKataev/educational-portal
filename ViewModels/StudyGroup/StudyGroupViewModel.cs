using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels
{
    public class StudyGroupViewModel
    {
		[Required(ErrorMessage = "Укажите название учебной группы")]
		public string Name { get; set; }

		public string Code { get; set; }

		[Required(ErrorMessage = "Укажите дату начала обучения")]
		public System.DateTime DateStart { get;set; }

		[Required(ErrorMessage = "Укажите дату окончания обучения")]
		public System.DateTime DateEnd { get; set; }

		[Required(ErrorMessage = "Укажите форму обучения")]
		public int FormEducationId { get; set; }
    }
}