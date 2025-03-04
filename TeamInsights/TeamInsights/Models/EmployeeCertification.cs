using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class EmployeeCertification
    {
        [Key]
        public int EmployeeCertificationID { get; set; } // surrogate key

        [ForeignKey("Person")]
        public int EmployeeID { get; set; }
        public virtual Person Employee { get; set; }

        [ForeignKey("Certification")]
        public int CertificationID { get; set; }
        public virtual Certification Certification { get; set; }

        public DateTime IssuedDate { get; set; }
    }
}
