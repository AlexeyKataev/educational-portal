namespace Dotnet.Models
{
	public class Student
	{
		public ulong Id { get; set; }
		public ulong UserId { get; set;}	
		public bool IsLearns { get; set; }
		public bool IsOnline { get; set; }
		public ulong StudySubgroupId { get; set; }
		public StudySubgroup StudySubgroup { get; set; }
	}
}
