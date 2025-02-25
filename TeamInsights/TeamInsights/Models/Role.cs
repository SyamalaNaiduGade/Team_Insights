using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<Contribution> Contributions { get; set; }

    }
}
