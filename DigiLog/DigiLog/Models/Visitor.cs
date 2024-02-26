using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiLog.Models
{
    public class Visitor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:\+?(\d{1,3}))?([0-9()\s-]+)$", ErrorMessage = "Invalid Phone number format")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "EmailAddress")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; } 
        public string ReasonForVisitDescription { get; set; }
        public Photo Photo { get; set; }
        public DateTime ArrivalTime { get; set; } = DateTime.Now;
        public DateTime TagAssignedDateTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime DateCreated {  get; set; }
        public DateTime DateModified { get; set; }
      


        //Foreign Relationships
        [ForeignKey("Employee")]
        [Required]
        public string EmployeeNumber { get; set; } = string.Empty;
        public Employee? Employee { get; set; }
        [ForeignKey("Tag")]
        public string? TagNumber { get; set; }
        public Tag? Tag { get; set; }

        [ForeignKey("ReasonForVisit")]
        [Required]
        public long ReasonForVisitId { get; set; }
        public ReasonForVisit ReasonForVisit { get; set; }

        public long DepartmentId { get; set; }
        public Department? Department { get; set; }

        public string EmployeeName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        
    }

    
}
