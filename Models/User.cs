namespace CMCS.Models
{
    public class User
    {
        public int Id { get; set; }                     // Unique identifier
        public string Username { get; set; }            // For login
        public string Password { get; set; }            // Plaintext for demo
        public string Role { get; set; }                // Lecturer / Coordinator / Manager / HR
        public string Email { get; set; }               // For HR edit view
        public double HourlyRate { get; set; }          // Lecturer-specific
    }
}
