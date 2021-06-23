using System.Text.Json.Serialization;

namespace Dotnet.Models.Messenger
{
	public class MessageAttachedFile
	{
		public ulong Id { get; set; }
		public ulong MessageId { get; set; }
		[JsonIgnore]
		public UserMessage UserMessage { get; set; }
		public ulong AttachedFileId { get; set; }
		[JsonIgnore]
		public File File { get; set; }
	}
}