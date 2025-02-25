using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Evaluation
    {
        [Key]
        public int EvaluationID { get; set; }

        [ForeignKey("Contributions")]
        public int ContributionID { get; set; }
        public virtual Contribution Contributions { get; set; }

        [ForeignKey("Manager")]
        public int ManagerID { get; set; }
        public virtual Manager Manager { get; set; }

        [ForeignKey("Skills")]
        public int SkillID { get; set; }
        public virtual Skill Skills { get; set; }

        public int Score { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
