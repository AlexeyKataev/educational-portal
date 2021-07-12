namespace Dotnet.Models
{
	public class FileWork
	{
		public long Id { get; set; }
		public long FileId { get; set; }
		public File File { get; set; }
		public long WorkId { get; set; }
		public Work Work { get; set; }
	}
}
