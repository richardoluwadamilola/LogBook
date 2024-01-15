using DigiLog.Models;
using System.ComponentModel.DataAnnotations;

namespace DigiLog.DTOs
{
    public class VisitorDTO
    {
        public long Id;
        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required]
        [Display(Name = "Contact Address")]
        public string? ContactAddress { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Invalid Phone number format")]
        public string? PhoneNumber { get; set; }
        public string EmployeeNumber { get; set; }
        [Required]
        [Display(Name = "Reason for Visit")]
        public ReasonForVisit ReasonForVisit { get; set; }
        public string? ReasonForVisitDescription { get; set; }
        [Required]
        [Display(Name = "Photo (Base64 Encoded)")]
        public byte[]? Photo { get; set; }
        //public int? TagNumber { get; set; }

    }
}
