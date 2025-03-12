using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Certification
    {
        [Key]
        public int CertificationID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Certificate Name")]
        public string? CertificationName { get; set; }

        [Url]
        [StringLength(200)]
        public string? URL { get; set; }

        // Navigation properties
        public virtual ICollection<EmployeeCertification>? EmployeeCertifications { get; set; }
    }
}
