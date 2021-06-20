namespace Dotnet.ViewModels.API.Education
{
	public class StudentViewModel
	{
        public int Id { get; set; } = 0;

        public int UserId { get; set; } = 0;

        public bool IsLearns { get; set; } = true;

        public bool IsOnline { get; set; } = false;

        public StudySubgroupViewModel StudySubgroup { get; set; } = new StudySubgroupViewModel();
	}
}
