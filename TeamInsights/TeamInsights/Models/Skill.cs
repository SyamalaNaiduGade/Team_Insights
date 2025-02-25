using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //Navigation Property
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
    }
}
