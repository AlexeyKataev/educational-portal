namespace Dotnet.Models
{
	public class FileWork
	{
		public ulong Id { get; set; }
		public ulong FileId { get; set; }
		public File File { get; set; }
		public ulong WorkId { get; set; }
		public Work Work { get; set; }
	}
}
