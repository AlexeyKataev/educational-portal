using Dotnet.ViewModels.API.App;

namespace Dotnet.ViewModels.API.Education
{
	public class StudentViewModel
	{
        public long Id { get; set; } = 0;

        public UserViewModel User { get; set; } = new UserViewModel();

        public bool IsLearns { get; set; } = true;

        public bool IsOnline { get; set; } = false;

        public StudySubgroupViewModel StudySubgroup { get; set; } = new StudySubgroupViewModel();
	}
}
