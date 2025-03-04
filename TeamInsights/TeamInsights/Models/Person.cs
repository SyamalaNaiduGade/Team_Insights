using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamInsights.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string StreetAddress { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        public DateTime HireDate { get; set; }

        [StringLength(500)]
        public string Experience { get; set; }

        public decimal Salary { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerID { get; set; } // Nullable Foreign Key
        public virtual Person Manager { get; set; }

        // Navigation properties
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
        public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        public virtual ICollection<Performance> Performances { get; set; } //As Manager

    }
}
