using System.Text.Json.Serialization;

namespace Dotnet.Models.Messenger
{
	public class MessageAttachedFile
	{
		public long Id { get; set; }
		public long MessageId { get; set; }
		[JsonIgnore]
		public UserMessage UserMessage { get; set; }
		public long AttachedFileId { get; set; }
		[JsonIgnore]
		public File File { get; set; }
	}
}