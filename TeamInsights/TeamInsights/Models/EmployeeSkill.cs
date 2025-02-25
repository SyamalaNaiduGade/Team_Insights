using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class EmployeeSkill
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Skills")]
        public int SkillID { get; set; }
        public virtual Skill Skill { get; set; }

        public int SkillLevel { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AcquiredDate { get; set; }

        [ForeignKey("Certification")]
        public int? CertificationID { get; set; } //Nullable
        public virtual Certification Certification { get; set; }


    }
}
