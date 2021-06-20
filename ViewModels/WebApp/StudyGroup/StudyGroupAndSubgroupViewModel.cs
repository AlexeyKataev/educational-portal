using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.StudyGroup
{
    public class StudyGroupAndSubgroupViewModel
    {
		public StudyGroupViewModel StudyGroupViewModel { get; set; } = null;
		public StudySubgroupViewModel StudySubgroupViewModel { get; set; } = null;
    }
}