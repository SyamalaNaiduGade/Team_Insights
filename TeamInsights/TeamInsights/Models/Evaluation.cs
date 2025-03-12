using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Evaluation
    {
        [Key]
        public int EvaluationID { get; set; }

        public int Score { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Evaluation Date")]
        public DateTime EvaluationDate { get; set; }

        // Navigation properties
        public virtual ICollection<Performance> Performances { get; set; }
    }
}
