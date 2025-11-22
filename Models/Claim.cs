using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Lecturer name is required.")]
        [StringLength(100)]
        [Display(Name = "Lecturer Name")]
        public string LecturerName { get; set; }

        [Required]
        [Range(1, 200, ErrorMessage = "Hours must be between 1 and 200.")]
        [Display(Name = "Hours Worked")]
        public double HoursWorked { get; set; }

        [Required]
        [Range(50, 1000, ErrorMessage = "Hourly rate must be between R50 and R1000.")]
        [Display(Name = "Hourly Rate (R)")]
        public double HourlyRate { get; set; }

        [Display(Name = "Total Pay (R)")]
        public double TotalPay { get; set; }   // No validation here

        [Display(Name = "Status")]
        public string? Status { get; set; }    // Now optional (important)

        [Display(Name = "Submitted Date")]
        public DateTime SubmittedDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string? DocumentPath { get; set; }
    }
}
