using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Certification
    {
        [Key]
        public int CertificationID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //Navigation Property
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
