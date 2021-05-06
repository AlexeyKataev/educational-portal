using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.Work
{
    public class WorkViewModel
    {
        [Required(ErrorMessage = "Укажите описание для задания.")]
		public string Description { get; set; }

		[Required]
		public bool IsObligation { get; set; } = false;

		public System.DateTime DateAdded { get; set; } = System.DateTime.Now;

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

		[Required(ErrorMessage = "Укажите учебный предмет.")]
		public int SubjectId { get; set; }

		[Required(ErrorMessage = "Укажите тип работы.")]
		public int TypeWorksId { get; set; }

		private int[] studySubgroupId;
		[Required]
		public int[] StudySubgroupsId 
		{
			get => studySubgroupId;
			set 
			{
				if (value.Length > 0) studySubgroupId = value;
			}
		}	
    }
}