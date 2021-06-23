using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.StudyGroup
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

		[Required(ErrorMessage = "Укажите специальность")]
		public ulong FormEducationId { get; set; }

		[Required(ErrorMessage = "Укажите форму обучения")]
		public ulong SpecialtyId { get; set; }
    }
}