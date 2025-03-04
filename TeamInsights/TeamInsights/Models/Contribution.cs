using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Contribution
    {
        [Key]
        public int ContributionID { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Contribution Date")]
        public DateTime ContributionDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        //Navigation Property
        public virtual ICollection<Performance> Performances { get; set; }
    }
}
