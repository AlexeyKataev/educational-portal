namespace Dotnet.ViewModels.API.Education
{
	public class StudentViewModel
	{
        public long Id { get; set; } = 0;

        public long UserId { get; set; } = 0;

        public bool IsLearns { get; set; } = true;

        public bool IsOnline { get; set; } = false;

        public StudySubgroupViewModel StudySubgroup { get; set; } = new StudySubgroupViewModel();
	}
}
