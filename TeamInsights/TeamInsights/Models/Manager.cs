using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Manager
    {
        [Key]
        public int ManagerID { get; set; }

        public string Experience { get; set; }

        [ForeignKey("Person")]
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

        //Navigation Properties
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
    }
}
