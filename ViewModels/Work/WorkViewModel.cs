using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.Work
{
    public class WorkViewModel
    {
        [Required(ErrorMessage = "Укажите описание для задания.")]
		public string Description { get; set; }

		[Required]
		public bool IsObligation { get; set; } = false;

		public System.DateTime DateAdded { get; set; } = System.DateTime.Today;

		public System.DateTime DateDeparture { get; set; } = new System.DateTime(0001, 01, 01, 01, 01, 01);

		private byte countAttempts = 1;
		public byte CountAttempts 
		{
			get => countAttempts;
			set
			{
				if (value == 0 || value < 0) countAttempts = 1;
				else if (value >= 255) countAttempts = 255;
				else countAttempts = value;
			}
		}

		[Required]
		public int[] StudySubgroupsId { get; set; }
    }
}