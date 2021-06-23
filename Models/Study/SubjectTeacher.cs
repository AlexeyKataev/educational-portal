namespace Dotnet.Models.Study 
{
	public class SubjectTeacher 
	{
		public ulong Id { get; set; }
		public ulong TeacherId { get; set; }
		public Teacher Teacher { get; set; }
		public ulong SubjectId { get; set; }
		public Subject Subject { get; set; }
	}
}