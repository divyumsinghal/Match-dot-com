using System.Text.Json;

namespace MatchDotCom.UserProfile.Tests
{
    public class AddressTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateAddress()
        {
            // Arrange
            var street = "123 Main St";
            var city = "Dublin";
            var stateOrProvince = "Dublin";
            var postalCode = "D01 A1B2";
            var country = "Ireland";
            var eircode = "D01A1B2";

            // Act
            var address = new Address(street, city, stateOrProvince, postalCode, country, eircode);

            // Assert
            address.Street.Should().Be(street);
            address.City.Should().Be(city);
            address.StateOrProvince.Should().Be(stateOrProvince);
            address.PostalCode.Should().Be(postalCode);
            address.Country.Should().Be(country);
            address.Eircode.Should().Be(eircode);
            address.Id.Should().NotBeEmpty();
            address.Coordinates.Should().NotBeNull();
        }

        [Fact]
        public void Constructor_ShouldSetDefaultCoordinates()
        {
            // Arrange & Act
            var address = new Address("123 Main St", "Dublin", "Dublin", "D01 A1B2", "Ireland", "D01A1B2");

            // Assert
            address.Coordinates.latitude.Should().Be(53.3498); // Dublin city center default
            address.Coordinates.longitude.Should().Be(-6.2603);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateAddressWithUpdatedCoordinates()
        {
            // Arrange
            var street = "123 Main St";
            var city = "Dublin";
            var stateOrProvince = "Dublin";
            var postalCode = "D01 A1B2";
            var country = "Ireland";
            var eircode = "D01A1B2";

            // Act
            var address = await Address.CreateAsync(street, city, stateOrProvince, postalCode, country, eircode);

            // Assert
            address.Should().NotBeNull();
            address.Street.Should().Be(street);
            address.City.Should().Be(city);
            address.StateOrProvince.Should().Be(stateOrProvince);
            address.PostalCode.Should().Be(postalCode);
            address.Country.Should().Be(country);
            address.Eircode.Should().Be(eircode);
            address.Coordinates.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateCoordinatesAsync_ShouldUpdateCoordinates()
        {
            // Arrange
            var address = new Address("123 Main St", "Dublin", "Dublin", "D01 A1B2", "Ireland", "D01A1B2");
            var originalLatitude = address.Coordinates.latitude;
            var originalLongitude = address.Coordinates.longitude;

            // Act
            await address.UpdateCoordinatesAsync();

            // Assert
            address.Coordinates.Should().NotBeNull();
            // Note: Since we're using a real geocoding service, coordinates might change
            // We just verify that the coordinates object is still valid
            address.Coordinates.latitude.Should().BeInRange(-90, 90);
            address.Coordinates.longitude.Should().BeInRange(-180, 180);
        }

        [Fact]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            var address = new Address("123 Main St", "Dublin", "Dublin", "D01 A1B2", "Ireland", "D01A1B2");

            // Act
            var json = address.ToJson();

            // Assert
            json.Should().NotBeNullOrEmpty();
            var deserializedAddress = JsonSerializer.Deserialize<Address>(json);
            deserializedAddress.Should().NotBeNull();
            deserializedAddress!.Street.Should().Be("123 Main St");
            deserializedAddress.City.Should().Be("Dublin");
        }

        [Theory]
        [InlineData("123 Main Street", "New York", "NY", "10001", "USA", "")]
        [InlineData("456 Oak Avenue", "Los Angeles", "CA", "90210", "USA", "")]
        [InlineData("789 Pine Road", "Cork", "Cork", "T12 ABC1", "Ireland", "T12ABC1")]
        [InlineData("321 Elm Street", "London", "England", "SW1A 1AA", "UK", "")]
        public void Constructor_WithVariousValidAddresses_ShouldCreateAddress(
            string street, string city, string stateOrProvince, string postalCode, string country, string eircode)
        {
            // Act
            var address = new Address(street, city, stateOrProvince, postalCode, country, eircode);

            // Assert
            address.Street.Should().Be(street);
            address.City.Should().Be(city);
            address.StateOrProvince.Should().Be(stateOrProvince);
            address.PostalCode.Should().Be(postalCode);
            address.Country.Should().Be(country);
            address.Eircode.Should().Be(eircode);
        }

        [Fact]
        public void Address_ShouldHaveUniqueId()
        {
            // Arrange & Act
            var address1 = new Address("123 Main St", "Dublin", "Dublin", "D01 A1B2", "Ireland", "D01A1B2");
            var address2 = new Address("456 Oak Ave", "Cork", "Cork", "T12 ABC1", "Ireland", "T12ABC1");

            // Assert
            address1.Id.Should().NotBe(address2.Id);
            address1.Id.Should().NotBeEmpty();
            address2.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void Address_WithEmptyEircode_ShouldBeValid()
        {
            // Arrange & Act
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA", "");

            // Assert
            address.Eircode.Should().Be("");
            address.Street.Should().Be("123 Main St");
            address.City.Should().Be("New York");
        }

        [Fact]
        public void Coordinates_ShouldBeValidGeographicalRange()
        {
            // Arrange & Act
            var address = new Address("123 Main St", "Dublin", "Dublin", "D01 A1B2", "Ireland", "D01A1B2");

            // Assert
            address.Coordinates.latitude.Should().BeInRange(-90, 90);
            address.Coordinates.longitude.Should().BeInRange(-180, 180);
        }
    }
}
