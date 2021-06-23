namespace Dotnet.Models.Study 
{
	public class StudySubgroupWork 
	{
		public ulong Id { get; set; }
		public ulong WorkId { get; set; }
		public Work Work { get; set; }
		public ulong StudySubgroupId { get; set; }
		public StudySubgroup StudySubgroup { get; set; }
	}
}