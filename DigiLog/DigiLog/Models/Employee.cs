using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiLog.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string EmployeeNumber { get; set; } = string.Empty;
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Department")]
        public string Department { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
