namespace Dotnet.Models
{
	public class Student
	{
		public int Id { get; set; }
		public int UserId { get; set;}	
		public bool IsLearns { get; set; }
		public bool IsOnline { get; set; }
		public int StudySubgroupId { get; set; }
		public StudySubgroup StudySubgroup { get; set; }
	}
}
