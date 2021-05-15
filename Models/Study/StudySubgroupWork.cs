namespace Dotnet.Models.Study 
{
	public class StudySubgroupWork 
	{
		public int Id { get; set; }
		public int WorkId { get; set; }
		public Work Work { get; set; }
		public int StudySubgroupId { get; set; }
		public StudySubgroup StudySubgroup { get; set; }
	}
}