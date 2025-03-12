using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Skill Name")]
        public string SkillName { get; set; }

        [StringLength(100)]
        [Display(Name = "Skill Level")]
        public string SkillLevel { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Navigation properties
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
