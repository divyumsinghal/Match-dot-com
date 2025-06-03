namespace MatchDotCom.UserProfile.Tests
{
    public class CoordinatesTests
    {
        [Fact]
        public void Coordinates_WithValidLatitudeAndLongitude_ShouldCreateSuccessfully()
        {
            // Arrange
            var latitude = 53.3498;
            var longitude = -6.2603;

            // Act
            var coordinates = new Coordinates
            {
                latitude = latitude,
                longitude = longitude
            };

            // Assert
            coordinates.latitude.Should().Be(latitude);
            coordinates.longitude.Should().Be(longitude);
        }

        [Theory]
        [InlineData(0, 0)] // Equator and Prime Meridian
        [InlineData(90, 180)] // North Pole, International Date Line
        [InlineData(-90, -180)] // South Pole, opposite side of International Date Line
        [InlineData(53.3498, -6.2603)] // Dublin, Ireland
        [InlineData(40.7128, -74.0060)] // New York City
        [InlineData(-33.8688, 151.2093)] // Sydney, Australia
        public void Coordinates_WithValidWorldCoordinates_ShouldCreateSuccessfully(double lat, double lng)
        {
            // Act
            var coordinates = new Coordinates
            {
                latitude = lat,
                longitude = lng
            };

            // Assert
            coordinates.latitude.Should().Be(lat);
            coordinates.longitude.Should().Be(lng);
        }

        [Fact]
        public void Coordinates_LatitudeShouldBeInValidRange()
        {
            // Arrange & Act
            var coordinates = new Coordinates
            {
                latitude = 45.0,
                longitude = 90.0
            };

            // Assert
            coordinates.latitude.Should().BeInRange(-90, 90);
        }

        [Fact]
        public void Coordinates_LongitudeShouldBeInValidRange()
        {
            // Arrange & Act
            var coordinates = new Coordinates
            {
                latitude = 45.0,
                longitude = 120.0
            };

            // Assert
            coordinates.longitude.Should().BeInRange(-180, 180);
        }

        [Theory]
        [InlineData(91, 0)] // Invalid latitude (too high)
        [InlineData(-91, 0)] // Invalid latitude (too low)
        [InlineData(0, 181)] // Invalid longitude (too high)
        [InlineData(0, -181)] // Invalid longitude (too low)
        public void Coordinates_WithOutOfRangeValues_ShouldStillCreate(double lat, double lng)
        {
            // Note: The Coordinates class doesn't seem to have validation,
            // so this test documents the current behavior

            // Act
            var coordinates = new Coordinates
            {
                latitude = lat,
                longitude = lng
            };

            // Assert
            coordinates.latitude.Should().Be(lat);
            coordinates.longitude.Should().Be(lng);
        }

        [Fact]
        public void Coordinates_WithPreciseValues_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseLatitude = 53.34980123456789;
            var preciseLongitude = -6.26031987654321;

            // Act
            var coordinates = new Coordinates
            {
                latitude = preciseLatitude,
                longitude = preciseLongitude
            };

            // Assert
            coordinates.latitude.Should().BeApproximately(preciseLatitude, 1e-15);
            coordinates.longitude.Should().BeApproximately(preciseLongitude, 1e-15);
        }

        [Fact]
        public void Coordinates_Equality_ShouldWorkCorrectly()
        {
            // Arrange
            var coords1 = new Coordinates { latitude = 53.3498, longitude = -6.2603 };
            var coords2 = new Coordinates { latitude = 53.3498, longitude = -6.2603 };
            var coords3 = new Coordinates { latitude = 40.7128, longitude = -74.0060 };

            // Assert
            coords1.latitude.Should().Be(coords2.latitude);
            coords1.longitude.Should().Be(coords2.longitude);
            coords1.latitude.Should().NotBe(coords3.latitude);
            coords1.longitude.Should().NotBe(coords3.longitude);
        }

        [Fact]
        public void Coordinates_WithZeroValues_ShouldCreateSuccessfully()
        {
            // Act
            var coordinates = new Coordinates
            {
                latitude = 0.0,
                longitude = 0.0
            };

            // Assert
            coordinates.latitude.Should().Be(0.0);
            coordinates.longitude.Should().Be(0.0);
        }

        [Fact]
        public void Coordinates_WithNegativeValues_ShouldCreateSuccessfully()
        {
            // Arrange
            var latitude = -25.2744; // São Paulo, Brazil
            var longitude = -51.1696;

            // Act
            var coordinates = new Coordinates
            {
                latitude = latitude,
                longitude = longitude
            };

            // Assert
            coordinates.latitude.Should().Be(latitude);
            coordinates.longitude.Should().Be(longitude);
            coordinates.latitude.Should().BeNegative();
            coordinates.longitude.Should().BeNegative();
        }

        [Fact]
        public void Coordinates_RequiredProperties_ShouldBeSet()
        {
            // Arrange & Act
            var coordinates = new Coordinates
            {
                latitude = 53.3498,
                longitude = -6.2603
            };

            // Assert
            coordinates.Should().NotBeNull();
            coordinates.latitude.Should().NotBe(default(double));
            coordinates.longitude.Should().NotBe(default(double));
        }
    }
}
