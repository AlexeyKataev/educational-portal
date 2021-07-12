using System.Collections.Generic;
using Dotnet.Models.Study;

namespace Dotnet.Models
{
	public class Teacher
	{
		public long Id { get; set; }
		public long UserId { get; set; }	
		private string specialization;
		public string Specialization 
		{  
			get => specialization ?? "Специальность не указана";
			set { specialization = value; }
		}
		private string post;
		public string Post
		{
			get => post ?? "Должность не указана";
			set { post = value; }
		}
		public List<Work> Works { get; set; } = new List<Work>();
		public List<SubjectTeacher> SubjectTeacher { get; set; } = new List<SubjectTeacher>();
	}
}
