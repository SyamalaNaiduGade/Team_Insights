using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(100)]
        public string? RoleName { get; set; }

        // Navigation properties
        public virtual ICollection<EmployeeRole>? EmployeeRoles { get; set; }

    }
}
