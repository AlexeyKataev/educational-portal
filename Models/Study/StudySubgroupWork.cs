namespace Dotnet.Models.Study 
{
	public class StudySubgroupWork 
	{
		public long Id { get; set; }
		public long WorkId { get; set; }
		public Work Work { get; set; }
		public long StudySubgroupId { get; set; }
		public StudySubgroup StudySubgroup { get; set; }
	}
}