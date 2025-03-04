using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class EmployeeSkill
    {
        [Key]
        public int EmployeeSkillID { get; set; } // surrogate key

        [ForeignKey("Person")]
        public int EmployeeID { get; set; }
        public virtual Person Employee { get; set; }

        [ForeignKey("Skill")]
        public int SkillID { get; set; }
        public virtual Skill Skill { get; set; }

        public DateTime AcquiredDate { get; set; }

    }
}
