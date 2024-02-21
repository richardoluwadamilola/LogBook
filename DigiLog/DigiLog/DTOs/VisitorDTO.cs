using DigiLog.Models;
using System.ComponentModel.DataAnnotations;

namespace DigiLog.DTOs
{
    public class VisitorDTO
    {
        public long Id {  get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:\+?(\d{1,3}))?([0-9()\s-]+)$", ErrorMessage = "Invalid Phone number format")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        [Display(Name = "Reason for Visit")]
        public string ReasonForVisit { get; set; }
        public string ReasonForVisitDescription { get; set; }
        [Display(Name = "Photo (Base64 Encoded)")]
        public string Photo { get; set; } = string.Empty;
        public DateTime ArrivalTime { get; internal set; } = DateTime.Now;
        public DateTime DepartureTime { get; internal set; }
        public ReasonForVisit ReasonForVisitEnum { get; set; }

    }
}
