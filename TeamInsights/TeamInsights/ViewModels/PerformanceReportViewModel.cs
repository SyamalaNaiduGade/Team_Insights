using TeamInsights.Models;

namespace TeamInsights.ViewModels
{
    public class PerformanceReportViewModel
    {
        public Person? Employee { get; set; }
        public List<Performance>? Performances { get; set; }

        public int Year { get; set; } // Add a Year property

    }
}
