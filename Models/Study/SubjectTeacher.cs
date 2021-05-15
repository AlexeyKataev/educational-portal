namespace Dotnet.Models.Study 
{
	public class SubjectTeacher 
	{
		public int Id { get; set; }
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }
		public int SubjectId { get; set; }
		public Subject Subject { get; set; }
	}
}