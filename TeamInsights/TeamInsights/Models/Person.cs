using System.ComponentModel.DataAnnotations;

namespace TeamInsights.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        [Required]
        [StringLength(100)] // Example constraint
        public string FName { get; set; }
        [Required]
        [StringLength(100)] // Example constraint
        public string LName { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(200)]
        public string State { get; set; }

        [StringLength(200)]
        public string Zipcode { get; set; }

        //Navigation Property
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Manager> Managers { get; set; }

    }
}
