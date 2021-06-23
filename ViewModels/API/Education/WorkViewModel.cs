using System;

namespace Dotnet.ViewModels.API.Education
{
	public class WorkViewModel
	{
		public ulong Id { get; set; } = 0;

		public string Description { get; set; } = "Нет описания";

		public bool IsObligation { get; set; } = true;

		public DateTime DateAdded { get; set; } = new DateTime(01, 01, 01, 01, 01, 01);

		public DateTime DateDeparture { get; set; } = new DateTime(01, 01, 01, 01, 01, 01);

		public byte CountAttempts { get; set; } = 1;

		public Dotnet.ViewModels.API.App.UserViewModel Teacher { get; set; } = new Dotnet.ViewModels.API.App.UserViewModel { };

		public string Subject { get; set; } = "";

		public string TypeWorks { get; set; } = "";

		public bool IsSetAttachment { get; set; } = false;

		public string AttachmentDownloadPath { get; set; } = "";
	}
}