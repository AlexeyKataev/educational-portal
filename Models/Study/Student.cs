namespace Dotnet.Models
{
	public class Student
	{
		public long Id { get; set; }
		public long UserId { get; set;}	
		public bool IsLearns { get; set; }
		public bool IsOnline { get; set; }
		public long StudySubgroupId { get; set; }
		public StudySubgroup StudySubgroup { get; set; }
	}
}
