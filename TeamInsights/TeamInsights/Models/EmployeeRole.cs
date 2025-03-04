using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class EmployeeRole
    {
        [Key]
        public int EmployeeRoleID { get; set; }

        [ForeignKey("Person")]
        public int EmployeeID { get; set; }
        public virtual Person Employee { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }

        // Navigation properties
        public virtual ICollection<Performance> Performances { get; set; }

    }
}
