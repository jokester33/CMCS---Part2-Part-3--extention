using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
namespace CMCS.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string StaffNumber { get; set; }
        public string Contact { get; set; }
        // Navigation
        public ICollection<Claim> Claims { get; set; }
    }
}

