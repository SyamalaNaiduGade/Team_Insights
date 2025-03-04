using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Performance
    {
        [Key]
        public int PerformanceID { get; set; }

        [ForeignKey("Manager")]
        public int ManagerID { get; set; } //FK to person table (as manager)
        public virtual Person Manager { get; set; }

        [ForeignKey("EmployeeRole")]
        public int EmployeeRoleID { get; set; }
        public virtual EmployeeRole EmployeeRole { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [ForeignKey("Contribution")]
        public int ContributionID { get; set; }
        public virtual Contribution Contribution { get; set; }

        [ForeignKey("Evaluation")]
        public int EvaluationID { get; set; }
        public virtual Evaluation Evaluation { get; set; }

        [Display(Name ="Hours Worked")]
        public double HoursWorked { get; set; }

        [DataType(DataType.Date)]
        public DateTime Year { get; set; }
    }
}
