using System.Text.Json;

namespace MatchDotCom.UserProfile.Tests
{
    public class ContactTests
    {
        private Address CreateValidAddress()
        {
            return new Address("123 Main St", "Dublin", "Dublin", "D01 A1B2", "Ireland", "D01A1B2");
        }

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateContact()
        {
            // Arrange
            var email = "test@example.com";
            var phoneNumber = "+353-1-234-5678";
            var address = CreateValidAddress();

            // Act
            var contact = new Contact(email, phoneNumber, address);

            // Assert
            contact.Email.Should().Be(email);
            contact.PhoneNumber.Should().Be(phoneNumber);
            contact.Address.Should().Be(address);
            contact.Id.Should().NotBeEmpty();
            contact.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            contact.UpdatedAt.Should().BeNull();
        }

        [Theory]
        [InlineData("test@example.com")]
        [InlineData("user.name@domain.co.uk")]
        [InlineData("test+tag@example.org")]
        [InlineData("simple@test.io")]
        public void Constructor_WithValidEmail_ShouldCreateContact(string validEmail)
        {
            // Arrange
            var phoneNumber = "+353-1-234-5678";
            var address = CreateValidAddress();

            // Act
            var contact = new Contact(validEmail, phoneNumber, address);

            // Assert
            contact.Email.Should().Be(validEmail);
        }

        [Theory]
        [InlineData("+353-1-234-5678")]
        [InlineData("123-456-7890")]
        [InlineData("+1-800-555-0123")]
        [InlineData("087-123-4567")]
        public void Constructor_WithValidPhoneNumber_ShouldCreateContact(string validPhoneNumber)
        {
            // Arrange
            var email = "test@example.com";
            var address = CreateValidAddress();

            // Act
            var contact = new Contact(email, validPhoneNumber, address);

            // Assert
            contact.PhoneNumber.Should().Be(validPhoneNumber);
        }

        [Fact]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            var email = "test@example.com";
            var phoneNumber = "+353-1-234-5678";
            var address = CreateValidAddress();
            var contact = new Contact(email, phoneNumber, address);

            // Act
            var json = contact.ToJson();

            // Assert
            json.Should().NotBeNullOrEmpty();
            var deserializedContact = JsonSerializer.Deserialize<Contact>(json);
            deserializedContact.Should().NotBeNull();
            deserializedContact!.Email.Should().Be(email);
            deserializedContact.PhoneNumber.Should().Be(phoneNumber);
        }

        [Fact]
        public void Contact_ShouldHaveRequiredProperties()
        {
            // Arrange
            var email = "test@example.com";
            var phoneNumber = "+353-1-234-5678";
            var address = CreateValidAddress();

            // Act
            var contact = new Contact(email, phoneNumber, address);

            // Assert
            contact.Should().NotBeNull();
            contact.Email.Should().NotBeNullOrEmpty();
            contact.PhoneNumber.Should().NotBeNullOrEmpty();
            contact.Address.Should().NotBeNull();
            contact.CreatedAt.Should().NotBe(default(DateTime));
        }

        [Fact]
        public void Contact_WithValidAddress_ShouldHaveCorrectAddressReference()
        {
            // Arrange
            var email = "test@example.com";
            var phoneNumber = "+353-1-234-5678";
            var address = CreateValidAddress();

            // Act
            var contact = new Contact(email, phoneNumber, address);

            // Assert
            contact.Address.Should().BeSameAs(address);
            contact.Address.Street.Should().Be("123 Main St");
            contact.Address.City.Should().Be("Dublin");
            contact.Address.Country.Should().Be("Ireland");
        }

        [Fact]
        public void Contact_CreatedAt_ShouldBeSetToCurrentTime()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var contact = new Contact("test@example.com", "+353-1-234-5678", CreateValidAddress());
            var afterCreation = DateTime.UtcNow;

            // Assert
            contact.CreatedAt.Should().BeOnOrAfter(beforeCreation);
            contact.CreatedAt.Should().BeOnOrBefore(afterCreation);
        }

        [Fact]
        public void Contact_UpdatedAt_ShouldBeNullByDefault()
        {
            // Arrange & Act
            var contact = new Contact("test@example.com", "+353-1-234-5678", CreateValidAddress());

            // Assert
            contact.UpdatedAt.Should().BeNull();
        }
    }
}
