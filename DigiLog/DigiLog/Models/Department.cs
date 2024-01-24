using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiLog.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DepartmentId { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        [Column(TypeName = "varchar(30)")]
        public string DepartmentName { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
