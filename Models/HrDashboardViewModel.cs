namespace CMCS.Models
{
    public class HrDashboardViewModel
    {
        public List<Claim> Claims { get; set; }
        public List<HrSummaryViewModel> Summary { get; set; }
    }

    public class HrSummaryViewModel
    {
        public string LecturerName { get; set; }
        public int TotalClaims { get; set; }
        public double TotalHours { get; set; }
        public double TotalPaid { get; set; }
    }
}
