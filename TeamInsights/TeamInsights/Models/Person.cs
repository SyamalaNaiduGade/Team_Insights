using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamInsights.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        [Required]
        [Display(Name ="First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Experience(Years)")]
        public float Experience { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerID { get; set; } // Nullable Foreign Key
        public virtual Person Manager { get; set; }
        
        //Inverse navigation for recursive relationship
        // One-to-many relationship (Manager -> Employees)
        public virtual ICollection<Person> Employees { get; set; }

        // Navigation properties
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
        public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; set; }
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        public virtual ICollection<Performance> Performances { get; set; } //As Manager
    }
}
