using Moq;
using System.Net;

namespace MatchDotCom.UserProfile.Tests
{
    public class GeocoderTests
    {
        [Fact]
        public async Task GetCoordinatesAsync_WithValidAddress_ShouldReturnCoordinates()
        {
            // Arrange
            var address = "Dublin, Ireland";

            // Act
            var result = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);

            // Assert
            result.Should().NotBeNull();
            if (result != null)
            {
                result.latitude.Should().BeInRange(-90, 90);
                result.longitude.Should().BeInRange(-180, 180);
            }
        }

        [Fact]
        public async Task GetCoordinatesAsync_WithEmptyAddress_ShouldReturnFallbackCoordinates()
        {
            // Arrange
            var address = "";

            // Act
            var result = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);

            // Assert
            result.Should().NotBeNull();
            if (result != null)
            {
                result.latitude.Should().BeInRange(-90, 90);
                result.longitude.Should().BeInRange(-180, 180);
            }
        }

        [Fact]
        public async Task GetCoordinatesAsync_WithNullAddress_ShouldHandleGracefully()
        {
            // Arrange
            string address = null!;

            // Act & Assert
            var act = async () => await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);

            // Should not throw an exception, but behavior may vary
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [InlineData("Dublin, Ireland")]
        [InlineData("Cork, Ireland")]
        [InlineData("Galway, Ireland")]
        [InlineData("123 Main Street, Dublin, Ireland")]
        public async Task GetCoordinatesAsync_WithVariousIrishAddresses_ShouldReturnValidCoordinates(string address)
        {
            // Act
            var result = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);

            // Assert
            result.Should().NotBeNull();
            if (result != null)
            {
                // Ireland is roughly between these coordinates
                result.latitude.Should().BeInRange(51.0, 56.0); // Approximate latitude range for Ireland
                result.longitude.Should().BeInRange(-11.0, -5.0); // Approximate longitude range for Ireland
            }
        }

        [Fact]
        public async Task GetCoordinatesAsync_WithAddressContainingSpecialCharacters_ShouldHandleCorrectly()
        {
            // Arrange
            var address = "O'Connell Street, Dublin, Ireland";

            // Act
            var result = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);

            // Assert
            result.Should().NotBeNull();
            if (result != null)
            {
                result.latitude.Should().BeInRange(-90, 90);
                result.longitude.Should().BeInRange(-180, 180);
            }
        }

        [Fact]
        public async Task GetCoordinatesAsync_WithAddressContainingExtraSpaces_ShouldHandleCorrectly()
        {
            // Arrange
            var address = "  Dublin   Ireland  ";

            // Act
            var result = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);

            // Assert
            result.Should().NotBeNull();
            if (result != null)
            {
                result.latitude.Should().BeInRange(-90, 90);
                result.longitude.Should().BeInRange(-180, 180);
            }
        }

        [Fact]
        public async Task GetCoordinatesAsync_WithVeryLongAddress_ShouldHandleCorrectly()
        {
            // Arrange
            var address = "This is a very long address that might be too long for the geocoding service to handle properly, Dublin, Ireland";

            // Act
            var result = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);

            // Assert
            result.Should().NotBeNull();
            if (result != null)
            {
                result.latitude.Should().BeInRange(-90, 90);
                result.longitude.Should().BeInRange(-180, 180);
            }
        }

        [Fact]
        public async Task GetCoordinatesAsync_WithNonExistentAddress_ShouldReturnFallbackCoordinates()
        {
            // Arrange
            var address = "ThisPlaceDoesNotExist123456, NowhereCountry";

            // Act
            var result = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);

            // Assert
            result.Should().NotBeNull();
            if (result != null)
            {
                result.latitude.Should().BeInRange(-90, 90);
                result.longitude.Should().BeInRange(-180, 180);
            }
        }

        [Fact]
        public void NominatimResult_ShouldHaveRequiredProperties()
        {
            // Arrange & Act
            var result = new MatchDotCom.LocationServices.NominatimResult
            {
                lat = "53.3498",
                lon = "-6.2603"
            };

            // Assert
            result.lat.Should().Be("53.3498");
            result.lon.Should().Be("-6.2603");
        }

        [Fact]
        public void NominatimResult_ShouldHandleVariousCoordinateFormats()
        {
            // Arrange & Act
            var result1 = new MatchDotCom.LocationServices.NominatimResult
            {
                lat = "53.3498123",
                lon = "-6.2603456"
            };

            var result2 = new MatchDotCom.LocationServices.NominatimResult
            {
                lat = "0.0",
                lon = "0.0"
            };

            // Assert
            result1.lat.Should().Be("53.3498123");
            result1.lon.Should().Be("-6.2603456");
            result2.lat.Should().Be("0.0");
            result2.lon.Should().Be("0.0");
        }

        // Note: These tests interact with real external services and may be slow
        // Consider using these as integration tests rather than unit tests
        [Fact]
        public async Task GetCoordinatesAsync_ShouldRespectRateLimit()
        {
            // Arrange
            var address1 = "Dublin, Ireland";
            var address2 = "Cork, Ireland";

            // Act
            var startTime = DateTime.UtcNow;
            var result1 = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address1);
            var result2 = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address2);
            var endTime = DateTime.UtcNow;

            // Assert
            result1.Should().NotBeNull();
            result2.Should().NotBeNull();

            // Should take at least 1 second due to rate limiting
            var timeTaken = endTime - startTime;
            timeTaken.Should().BeGreaterThan(TimeSpan.FromMilliseconds(900));
        }

        [Fact]
        public async Task GetCoordinatesAsync_MultipleCallsWithSameAddress_ShouldReturnConsistentResults()
        {
            // Arrange
            var address = "Dublin, Ireland";

            // Act
            var result1 = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);
            await Task.Delay(1100); // Respect rate limit
            var result2 = await MatchDotCom.LocationServices.Geocoder.GetCoordinatesAsync(address);

            // Assert
            result1.Should().NotBeNull();
            result2.Should().NotBeNull();

            if (result1 != null && result2 != null)
            {
                result1.latitude.Should().BeApproximately(result2.latitude, 0.001);
                result1.longitude.Should().BeApproximately(result2.longitude, 0.001);
            }
        }
    }
}
