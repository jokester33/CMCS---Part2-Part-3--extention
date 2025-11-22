using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CMCS.Models
{
    public class ClaimDetail
    {
        public int ClaimDetailId { get; set; }
        [Required]
        public int ClaimId { get; set; }
        [Required]
        public double HoursWorked { get; set; }
        [Required]
        public double HourlyRate { get; set; }
        [NotMapped]
        public double TotalAmount => HoursWorked * HourlyRate;
        // Navigation
        public Claim Claim { get; set; }
    }
}

