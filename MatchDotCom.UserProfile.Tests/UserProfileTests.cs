using System.Text.Json;

namespace MatchDotCom.UserProfile.Tests
{
    public class UserProfileTests
    {
        private Contact CreateValidContact()
        {
            var coordinates = new Coordinates { latitude = 53.3498, longitude = -6.2603 };
            var address = new Address("123 Main St", "Dublin", "Dublin", "D01 A1B2", "Ireland", "D01A1B2");
            return new Contact("test@example.com", "+353-1-234-5678", address);
        }

        private ProfileBio CreateValidProfileBio()
        {
            var interests = new List<Interests> { Interests.Music, Interests.Travel };
            return new ProfileBio(
                "This is a very long bio that contains more than 500 characters. I love traveling around the world and experiencing different cultures. Music is my passion and I play guitar in my free time. I enjoy hiking, reading books, cooking various cuisines, and spending time with friends and family. I'm looking for someone who shares similar interests and values meaningful conversations. Life is an adventure and I want to share it with the right person who appreciates both quiet moments and exciting adventures.",
                "Live, love, laugh",
                GenderOptions.Male,
                new List<GenderOptions> { GenderOptions.Female },
                interests
            );
        }

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateUserProfile()
        {
            // Arrange
            var username = "testuser123";
            var firstName = "John";
            var middleName = "Michael";
            var lastName = "Doe";
            var dateOfBirth = new DateTime(1990, 1, 1);
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();

            // Act
            var userProfile = new MatchDotCom.UserProfile.UserProfile(username, firstName, middleName, lastName, dateOfBirth, contact, bio);

            // Assert
            userProfile.Username.Should().Be(username);
            userProfile.FirstName.Should().Be(firstName);
            userProfile.MiddleName.Should().Be(middleName);
            userProfile.LastName.Should().Be(lastName);
            userProfile.DateOfBirth.Should().Be(dateOfBirth);
            userProfile.Contact.Should().Be(contact);
            userProfile.Bio.Should().Be(bio);
            userProfile.Id.Should().NotBeEmpty();
            userProfile.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("")]
        [InlineData("verylongusernamethatexceedstwentycharacters")]
        [InlineData("user name")]
        [InlineData("user@name")]
        [InlineData("user-name")]
        public void Constructor_WithInvalidUsername_ShouldThrowArgumentException(string username)
        {
            // Arrange
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new MatchDotCom.UserProfile.UserProfile(username, "John", "Michael", "Doe", new DateTime(1990, 1, 1), contact, bio));

            exception.Message.Should().Contain("Username must be between 3 and 20 characters long and can only contain letters, numbers, and underscores.");
        }

        [Theory]
        [InlineData("J")]
        [InlineData("")]
        [InlineData("ThisIsAVeryLongFirstNameThatExceedsFiftyCharactersLimit")]
        public void Constructor_WithInvalidFirstName_ShouldThrowArgumentException(string firstName)
        {
            // Arrange
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new MatchDotCom.UserProfile.UserProfile("testuser", firstName, "Michael", "Doe", new DateTime(1990, 1, 1), contact, bio));

            exception.Message.Should().Contain("First name must be between 2 and 50 characters long");
        }

        [Theory]
        [InlineData("")]
        [InlineData("ThisIsAVeryLongMiddleNameThatExceedsFiftyCharactersLimit")]
        public void Constructor_WithInvalidMiddleName_ShouldThrowArgumentException(string middleName)
        {
            // Arrange
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new MatchDotCom.UserProfile.UserProfile("testuser", "John", middleName, "Doe", new DateTime(1990, 1, 1), contact, bio));

            exception.Message.Should().Contain("Middle name must be between 1 and 50 characters long");
        }

        [Theory]
        [InlineData("D")]
        [InlineData("")]
        [InlineData("ThisIsAVeryLongLastNameThatExceedsFiftyCharactersLimit")]
        public void Constructor_WithInvalidLastName_ShouldThrowArgumentException(string lastName)
        {
            // Arrange
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new MatchDotCom.UserProfile.UserProfile("testuser", "John", "Michael", lastName, new DateTime(1990, 1, 1), contact, bio));

            exception.Message.Should().Contain("Last name must be between 2 and 50 characters long");
        }

        [Fact]
        public void Constructor_WithUnderageUser_ShouldThrowArgumentException()
        {
            // Arrange
            var dateOfBirth = DateTime.UtcNow.AddYears(-17); // 17 years old
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new MatchDotCom.UserProfile.UserProfile("testuser", "John", "Michael", "Doe", dateOfBirth, contact, bio));

            exception.Message.Should().Contain("Date of birth must be atleast 18 years old");
        }

        [Fact]
        public void Constructor_WithVeryOldUser_ShouldThrowArgumentException()
        {
            // Arrange
            var dateOfBirth = new DateTime(1899, 12, 31); // Before 1900
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new MatchDotCom.UserProfile.UserProfile("testuser", "John", "Michael", "Doe", dateOfBirth, contact, bio));

            exception.Message.Should().Contain("Date of birth must be atleast 18 years old");
        }

        [Fact]
        public void Constructor_WithNullContact_ShouldThrowArgumentNullException()
        {
            // Arrange
            var bio = CreateValidProfileBio();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MatchDotCom.UserProfile.UserProfile("testuser", "John", "Michael", "Doe", new DateTime(1990, 1, 1), null!, bio));
        }

        [Fact]
        public void Constructor_WithNullBio_ShouldThrowArgumentNullException()
        {
            // Arrange
            var contact = CreateValidContact();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MatchDotCom.UserProfile.UserProfile("testuser", "John", "Michael", "Doe", new DateTime(1990, 1, 1), contact, null!));
        }

        [Fact]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();
            var userProfile = new MatchDotCom.UserProfile.UserProfile("testuser", "John", "Michael", "Doe", new DateTime(1990, 1, 1), contact, bio);

            // Act
            var json = userProfile.ToJson();

            // Assert
            json.Should().NotBeNullOrEmpty();
            var deserializedProfile = JsonSerializer.Deserialize<MatchDotCom.UserProfile.UserProfile>(json);
            deserializedProfile.Should().NotBeNull();
            deserializedProfile!.Username.Should().Be("testuser");
            deserializedProfile.FirstName.Should().Be("John");
        }

        [Fact]
        public void ValidUsername_ShouldBeAccepted()
        {
            // Arrange
            var validUsernames = new[] { "abc", "user123", "test_user", "User_Name_123", "abcdefghij1234567890" };
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();

            // Act & Assert
            foreach (var username in validUsernames)
            {
                var action = () => new MatchDotCom.UserProfile.UserProfile(username, "John", "Michael", "Doe", new DateTime(1990, 1, 1), contact, bio);
                action.Should().NotThrow($"Username '{username}' should be valid");
            }
        }

        [Fact]
        public void ValidDateOfBirth_ShouldBeAccepted()
        {
            // Arrange
            var exactly18YearsOld = DateTime.UtcNow.AddYears(-18).AddDays(-1);
            var contact = CreateValidContact();
            var bio = CreateValidProfileBio();

            // Act & Assert
            var action = () => new MatchDotCom.UserProfile.UserProfile("testuser", "John", "Michael", "Doe", exactly18YearsOld, contact, bio);
            action.Should().NotThrow("Exactly 18 years old should be valid");
        }
    }
}
