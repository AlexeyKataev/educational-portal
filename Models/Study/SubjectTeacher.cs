namespace Dotnet.Models.Study 
{
	public class SubjectTeacher 
	{
		public long Id { get; set; }
		public long TeacherId { get; set; }
		public Teacher Teacher { get; set; }
		public long SubjectId { get; set; }
		public Subject Subject { get; set; }
	}
}