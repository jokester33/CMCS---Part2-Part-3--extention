using System;
using System.ComponentModel.DataAnnotations;
namespace CMCS.Models
{
    public class Approval
    {
        public int ApprovalId { get; set; }
        [Required]
        public int ClaimId { get; set; }
        [Required]
        public string ApprovedBy { get; set; }
        public string Role { get; set; }
        public string Comments { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        // Navigation
        public Claim Claim { get; set; }
    }
}

