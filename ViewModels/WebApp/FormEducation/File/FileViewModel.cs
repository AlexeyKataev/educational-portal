using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.File
{
	public class FileViewModel
	{
		[Required(ErrorMessage = "Укажите название.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите маску.")]
		public string Pseudonym { get; set; }

		[Required(ErrorMessage = "Укажите путь.")]
		public string PathFile { get; set; }

		[Required(ErrorMessage = "Укажите видимость.")]
		public bool Vanish { get; set; } = false;

		[Required(ErrorMessage = "Укажите дату запроса на удаление.")]
		public System.DateTime NeedToDelete { get; set; } = new System.DateTime(0001, 01, 01, 01, 01, 01);

		public System.DateTime DateAdded { get; set; } = System.DateTime.Now;
	}
}
