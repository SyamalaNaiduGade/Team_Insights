using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [StringLength(100)]
        public string Designation { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [ForeignKey("Person")]
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

        // Navigation properties
        public virtual ICollection<Contribution> Contributions { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }

    }
}
