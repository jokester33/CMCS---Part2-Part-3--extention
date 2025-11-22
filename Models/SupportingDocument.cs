namespace CMCS.Models
{
    public class SupportingDocument
    {
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

        public int ClaimId { get; set; }
        public Claim? Claim { get; set; }
    }
}
