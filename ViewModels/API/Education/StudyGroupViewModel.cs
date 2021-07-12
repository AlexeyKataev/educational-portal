using System;

namespace Dotnet.ViewModels.API.Education
{
	public class StudyGroupViewModel
	{
		public long Id { get; set; } = 0;

		public string Name { get; set; } = "";

		public string Code { get; set; } = "";

		public DateTime DateStart { get; set; } = new DateTime(0001, 01, 01, 01, 01, 01);

		public DateTime DateEnd { get; set; } = new DateTime(0001, 01, 01, 01, 01, 01);

		public SpecialtyViewModel Specialty { get; set; } = new SpecialtyViewModel();
	}
}