using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.StudyGroup
{
    public class StudyViewModel
    {
		public StudyGroupViewModel StudyGroupViewModel { get; set; }
		public StudySubgroupViewModel StudySubgroupViewModel { get; set; }
    }
}