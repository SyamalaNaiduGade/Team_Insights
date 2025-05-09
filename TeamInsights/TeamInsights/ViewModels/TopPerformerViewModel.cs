using System.ComponentModel.DataAnnotations;

namespace TeamInsights.ViewModels
{
        public class TopPerformerViewModel
        {
            public int PersonID { get; set; }
            [Display(Name = "Employee Name")]
            public string EmployeeName { get; set; }
            [Display(Name = "Performance Score")]
            public double PerformanceScore { get; set; }
        }
    
}
