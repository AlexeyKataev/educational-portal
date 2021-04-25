using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.StudyGroup
{
    public class StudyGroupAndSubgroupViewModel
    {
		public StudyGroupViewModel StudyGroupViewModel { get; set; } = null;
		public StudySubgroupViewModel StudySubgroupViewModel { get; set; } = null;
    }
}