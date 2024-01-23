using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiLog.Models
{
    public class Visitor
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Display(Name  = "Full Name")]
        [Column(TypeName = "varchar(50)")]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "Invalid Phone number format")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Reason for Visit")]
        public ReasonForVisit ReasonForVisit { get; set; }
        public string ReasonForVisitDescription { get; set; }
        public Photo Photo { get; set; }
        public DateTime ArrivalTime { get; set; } = DateTime.Now;
        public DateTime DepartureTime { get; set; }
        public DateTime DateCreated {  get; set; }
        public DateTime DateModified { get; set; }


        //Foreign Relationships
        [ForeignKey("Employee")]
        [Required]
        public string EmployeeNumber { get; set; } = string.Empty;
        public Employee? Employee { get; set; } 

        [ForeignKey("Tag")]
        public string TagNumber { get; set; } = string.Empty;
        public Tag? Tag { get; set; }
    }

    public enum ReasonForVisit
    {
        [Display(Name = "Official Visit")]
        Official,
        [Display(Name = "Personal Visit")]
        Personal
    }
}
