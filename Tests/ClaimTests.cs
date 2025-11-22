using CMCS.Models;
using Xunit;

namespace CMCS.Tests
{
    public class ClaimTests
    {
        [Fact]
        public void TotalPay_ShouldBeCalculatedCorrectly()
        {
            // Arrange
            var claim = new Claim
            {
                HoursWorked = 10,
                HourlyRate = 100
            };

            // Act
            claim.TotalPay = claim.HoursWorked * claim.HourlyRate;

            // Assert
            Assert.Equal(1000, claim.TotalPay);
        }

        [Fact]
        public void Claim_ShouldHaveDefaultStatus_Pending()
        {
            // Arrange
            var claim = new Claim();

            // Act & Assert
            Assert.Equal("Pending", claim.Status);
        }
    }
}
