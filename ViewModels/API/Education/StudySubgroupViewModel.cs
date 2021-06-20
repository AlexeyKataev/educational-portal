namespace Dotnet.ViewModels.API.Education
{
	public class StudySubgroupViewModel
	{
		public int Id { get; set; } = 0;

		public string Name { get; set; } = "";

		public string Code { get; set; } = "";

		public StudyGroupViewModel StudyGroup { get; set; } = new StudyGroupViewModel();
	}
}