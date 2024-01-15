﻿using DigiLog.Models;
using System.ComponentModel.DataAnnotations;

namespace DigiLog.DTOs
{
    public class VisitorDTO
    {
        public long Id;
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Invalid Phone number format")]
        public string PhoneNumber { get; set; }
        public string EmployeeNumber { get; set; } = string.Empty;
        [Display(Name = "Reason for Visit")]
        public int ReasonForVisit { get; set; }
        public string ReasonForVisitDescription { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Photo (Base64 Encoded)")]
        public string Photo { get; set; } = string.Empty;
        //public int? TagNumber { get; set; }

    }
}
