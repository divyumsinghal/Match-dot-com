using System.Text.Json;

namespace MatchDotCom.UserProfile.Tests
{
    public class ProfileBioTests
    {
        private string CreateValidBioText()
        {
            return "This is a very long bio that contains more than 500 characters. I love traveling around the world and experiencing different cultures. Music is my passion and I play guitar in my free time. I enjoy hiking, reading books, cooking various cuisines, and spending time with friends and family. I'm looking for someone who shares similar interests and values meaningful conversations. Life is an adventure and I want to share it with the right person who appreciates both quiet moments and exciting adventures.";
        }

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateProfileBio()
        {
            // Arrange
            var bioText = CreateValidBioText();
            var lifeMotto = "Live, love, laugh";
            var gender = GenderOptions.Male;
            var genderPreference = new List<GenderOptions> { GenderOptions.Female };
            var interests = new List<Interests> { Interests.Music, Interests.Travel };

            // Act
            var profileBio = new ProfileBio(bioText, lifeMotto, gender, genderPreference, interests);

            // Assert
            profileBio.BioText.Should().Be(bioText);
            profileBio.LifeMotto.Should().Be(lifeMotto);
            profileBio.Gender.Should().Be(gender);
            profileBio.GenderPreference.Should().BeEquivalentTo(genderPreference);
            profileBio.Interests.Should().BeEquivalentTo(interests);
            profileBio.Id.Should().NotBeEmpty();
            profileBio.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            profileBio.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public void Constructor_WithNullLifeMotto_ShouldCreateProfileBio()
        {
            // Arrange
            var bioText = CreateValidBioText();
            var gender = GenderOptions.Female;
            var genderPreference = new List<GenderOptions> { GenderOptions.Male };
            var interests = new List<Interests> { Interests.Reading, Interests.Arts };

            // Act
            var profileBio = new ProfileBio(bioText, null, gender, genderPreference, interests);

            // Assert
            profileBio.BioText.Should().Be(bioText);
            profileBio.LifeMotto.Should().BeNull();
            profileBio.Gender.Should().Be(gender);
            profileBio.GenderPreference.Should().BeEquivalentTo(genderPreference);
            profileBio.Interests.Should().BeEquivalentTo(interests);
        }

        [Fact]
        public void Constructor_WithMultipleGenderPreferences_ShouldCreateProfileBio()
        {
            // Arrange
            var bioText = CreateValidBioText();
            var lifeMotto = "Carpe Diem";
            var gender = GenderOptions.Other;
            var genderPreference = new List<GenderOptions> { GenderOptions.Male, GenderOptions.Female, GenderOptions.Other };
            var interests = new List<Interests> { Interests.Technology, Interests.Gaming };

            // Act
            var profileBio = new ProfileBio(bioText, lifeMotto, gender, genderPreference, interests);

            // Assert
            profileBio.GenderPreference.Should().HaveCount(3);
            profileBio.GenderPreference.Should().Contain(GenderOptions.Male);
            profileBio.GenderPreference.Should().Contain(GenderOptions.Female);
            profileBio.GenderPreference.Should().Contain(GenderOptions.Other);
        }

        [Fact]
        public void Constructor_WithMultipleInterests_ShouldCreateProfileBio()
        {
            // Arrange
            var bioText = CreateValidBioText();
            var interests = new List<Interests>
            {
                Interests.Sports,
                Interests.Music,
                Interests.Travel,
                Interests.Food,
                Interests.Arts
            };

            // Act
            var profileBio = new ProfileBio(bioText, "Adventure awaits", GenderOptions.Male,
                new List<GenderOptions> { GenderOptions.Female }, interests);

            // Assert
            profileBio.Interests.Should().HaveCount(5);
            profileBio.Interests.Should().Contain(Interests.Sports);
            profileBio.Interests.Should().Contain(Interests.Music);
            profileBio.Interests.Should().Contain(Interests.Travel);
            profileBio.Interests.Should().Contain(Interests.Food);
            profileBio.Interests.Should().Contain(Interests.Arts);
        }

        [Theory]
        [InlineData(GenderOptions.Male)]
        [InlineData(GenderOptions.Female)]
        [InlineData(GenderOptions.Other)]
        public void Constructor_WithAllGenderOptions_ShouldCreateProfileBio(GenderOptions gender)
        {
            // Arrange
            var bioText = CreateValidBioText();
            var genderPreference = new List<GenderOptions> { GenderOptions.Male };
            var interests = new List<Interests> { Interests.Music };

            // Act
            var profileBio = new ProfileBio(bioText, "Test motto", gender, genderPreference, interests);

            // Assert
            profileBio.Gender.Should().Be(gender);
        }

        [Theory]
        [InlineData(Interests.Sports)]
        [InlineData(Interests.Music)]
        [InlineData(Interests.Travel)]
        [InlineData(Interests.Food)]
        [InlineData(Interests.Arts)]
        [InlineData(Interests.Technology)]
        [InlineData(Interests.Fitness)]
        [InlineData(Interests.Reading)]
        [InlineData(Interests.Gaming)]
        [InlineData(Interests.Outdoors)]
        public void Constructor_WithAllInterestTypes_ShouldCreateProfileBio(Interests interest)
        {
            // Arrange
            var bioText = CreateValidBioText();
            var genderPreference = new List<GenderOptions> { GenderOptions.Male };
            var interests = new List<Interests> { interest };

            // Act
            var profileBio = new ProfileBio(bioText, "Test motto", GenderOptions.Female, genderPreference, interests);

            // Assert
            profileBio.Interests.Should().Contain(interest);
        }

        [Fact]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            var bioText = CreateValidBioText();
            var profileBio = new ProfileBio(bioText, "Live life fully", GenderOptions.Male,
                new List<GenderOptions> { GenderOptions.Female },
                new List<Interests> { Interests.Music, Interests.Travel });

            // Act
            var json = profileBio.ToJson();

            // Assert
            json.Should().NotBeNullOrEmpty();
            var deserializedBio = JsonSerializer.Deserialize<ProfileBio>(json);
            deserializedBio.Should().NotBeNull();
            deserializedBio!.BioText.Should().Be(bioText);
            deserializedBio.LifeMotto.Should().Be("Live life fully");
            deserializedBio.Gender.Should().Be(GenderOptions.Male);
        }

        [Fact]
        public void ProfileBio_CreatedAt_ShouldBeSetToCurrentTime()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var profileBio = new ProfileBio(CreateValidBioText(), "Test", GenderOptions.Male,
                new List<GenderOptions> { GenderOptions.Female },
                new List<Interests> { Interests.Music });
            var afterCreation = DateTime.UtcNow;

            // Assert
            profileBio.CreatedAt.Should().BeOnOrAfter(beforeCreation);
            profileBio.CreatedAt.Should().BeOnOrBefore(afterCreation);
        }

        [Fact]
        public void ProfileBio_UpdatedAt_ShouldBeNullByDefault()
        {
            // Arrange & Act
            var profileBio = new ProfileBio(CreateValidBioText(), "Test", GenderOptions.Male,
                new List<GenderOptions> { GenderOptions.Female },
                new List<Interests> { Interests.Music });

            // Assert
            profileBio.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public void ProfileBio_ShouldHaveUniqueId()
        {
            // Arrange & Act
            var bio1 = new ProfileBio(CreateValidBioText(), "Test1", GenderOptions.Male,
                new List<GenderOptions> { GenderOptions.Female },
                new List<Interests> { Interests.Music });
            var bio2 = new ProfileBio(CreateValidBioText(), "Test2", GenderOptions.Female,
                new List<GenderOptions> { GenderOptions.Male },
                new List<Interests> { Interests.Travel });

            // Assert
            bio1.Id.Should().NotBe(bio2.Id);
            bio1.Id.Should().NotBeEmpty();
            bio2.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void ProfileBio_WithMaximumInterests_ShouldCreateSuccessfully()
        {
            // Arrange
            var bioText = CreateValidBioText();
            var allInterests = new List<Interests>
            {
                Interests.Sports, Interests.Music, Interests.Travel, Interests.Food,
                Interests.Arts, Interests.Technology, Interests.Fitness, Interests.Reading,
                Interests.Gaming, Interests.Outdoors
            };

            // Act
            var profileBio = new ProfileBio(bioText, "I love everything!", GenderOptions.Other,
                new List<GenderOptions> { GenderOptions.Male, GenderOptions.Female }, allInterests);

            // Assert
            profileBio.Interests.Should().HaveCount(10);
            profileBio.Interests.Should().BeEquivalentTo(allInterests);
        }

        [Fact]
        public void ProfileBio_WithEmptyGenderPreference_ShouldStillWork()
        {
            // Arrange
            var bioText = CreateValidBioText();
            var emptyGenderPreference = new List<GenderOptions>();
            var interests = new List<Interests> { Interests.Reading };

            // Act
            var profileBio = new ProfileBio(bioText, "Solo journey", GenderOptions.Other,
                emptyGenderPreference, interests);

            // Assert
            profileBio.GenderPreference.Should().BeEmpty();
            profileBio.Gender.Should().Be(GenderOptions.Other);
        }

        [Fact]
        public void ProfileBio_WithLongLifeMotto_ShouldCreateSuccessfully()
        {
            // Arrange
            var bioText = CreateValidBioText();
            var longMotto = "This is a very long life motto that contains many words and thoughts about how to live life to the fullest and embrace every moment with joy and passion for adventure";
            var interests = new List<Interests> { Interests.Reading };

            // Act
            var profileBio = new ProfileBio(bioText, longMotto, GenderOptions.Female,
                new List<GenderOptions> { GenderOptions.Male }, interests);

            // Assert
            profileBio.LifeMotto.Should().Be(longMotto);
            profileBio.LifeMotto!.Length.Should().BeLessOrEqualTo(200); // Assuming validation exists
        }
    }
}
