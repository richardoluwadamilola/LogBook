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
        [Column(TypeName = "varchar(20)")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Middle Name")]
        [Column(TypeName = "varchar(20)")]
        public string MiddleName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Last Name")]
        [Column(TypeName = "varchar(20)")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [ForeignKey("Department")]
        public long DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
