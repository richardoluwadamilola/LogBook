﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiLog.Models
{
    public class Visitor
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Display(Name  = "First Name")]
        public string? FirstName { get; set; }
        [Required]
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
        [RegularExpression(@"^\d{11}$", ErrorMessage ="Invalid Phone number format")]
        public string? PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Reason for Visit")]
        public ReasonForVisit ReasonForVisit { get; set; }
        public string? ReasonForVisitDescription { get; set; }
        [Required]
        [Display(Name = "Photo (Base64 Encoded)")]
        public byte[]? Photo {  get; set; }
        public DateTime ArrivalTime { get; set; } = DateTime.Now;
        public DateTime DepartureTime { get; set; }
        public DateTime DateCreated {  get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }


        //Foreign Relationships
        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [ForeignKey("Tag")]
        public long TagID { get; set; }
    }

    public enum ReasonForVisit
    {
        [Display(Name = "Official Visit")]
        Official,
        [Display(Name = "Personal Visit")]
        Personal
    }
}
