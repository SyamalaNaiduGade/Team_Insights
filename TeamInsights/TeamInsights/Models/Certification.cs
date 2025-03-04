using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Certification
    {
        [Key]
        public int CertificationID { get; set; }

        [Required]
        [StringLength(100)]
        public string CertificationName { get; set; }

        [StringLength(200)]
        public string URL { get; set; }

        // Navigation properties
        public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; set; }
    }
}
