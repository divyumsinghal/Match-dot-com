namespace MatchDotCom.UserProfile.Tests
{
    public class InterestsTests
    {
        [Fact]
        public void Interests_ShouldHaveAllExpectedValues()
        {
            // Arrange
            var expectedValues = new[]
            {
                Interests.Sports,
                Interests.Music,
                Interests.Travel,
                Interests.Food,
                Interests.Arts,
                Interests.Technology,
                Interests.Fitness,
                Interests.Reading,
                Interests.Gaming,
                Interests.Outdoors
            };

            // Act
            var actualValues = Enum.GetValues<Interests>();

            // Assert
            actualValues.Should().HaveCount(10);
            actualValues.Should().BeEquivalentTo(expectedValues);
        }

        [Theory]
        [InlineData(Interests.Sports, "Sports")]
        [InlineData(Interests.Music, "Music")]
        [InlineData(Interests.Travel, "Travel")]
        [InlineData(Interests.Food, "Food")]
        [InlineData(Interests.Arts, "Arts")]
        [InlineData(Interests.Technology, "Technology")]
        [InlineData(Interests.Fitness, "Fitness")]
        [InlineData(Interests.Reading, "Reading")]
        [InlineData(Interests.Gaming, "Gaming")]
        [InlineData(Interests.Outdoors, "Outdoors")]
        public void Interests_ShouldHaveCorrectStringRepresentation(Interests interest, string expectedString)
        {
            // Act
            var stringValue = interest.ToString();

            // Assert
            stringValue.Should().Be(expectedString);
        }

        [Fact]
        public void Interests_ShouldBeConvertibleToInt()
        {
            // Act & Assert
            ((int)Interests.Sports).Should().Be(0);
            ((int)Interests.Music).Should().Be(1);
            ((int)Interests.Travel).Should().Be(2);
            ((int)Interests.Food).Should().Be(3);
            ((int)Interests.Arts).Should().Be(4);
            ((int)Interests.Technology).Should().Be(5);
            ((int)Interests.Fitness).Should().Be(6);
            ((int)Interests.Reading).Should().Be(7);
            ((int)Interests.Gaming).Should().Be(8);
            ((int)Interests.Outdoors).Should().Be(9);
        }

        [Fact]
        public void Interests_ShouldBeConvertibleFromInt()
        {
            // Act & Assert
            ((Interests)0).Should().Be(Interests.Sports);
            ((Interests)1).Should().Be(Interests.Music);
            ((Interests)2).Should().Be(Interests.Travel);
            ((Interests)3).Should().Be(Interests.Food);
            ((Interests)4).Should().Be(Interests.Arts);
            ((Interests)5).Should().Be(Interests.Technology);
            ((Interests)6).Should().Be(Interests.Fitness);
            ((Interests)7).Should().Be(Interests.Reading);
            ((Interests)8).Should().Be(Interests.Gaming);
            ((Interests)9).Should().Be(Interests.Outdoors);
        }

        [Fact]
        public void Interests_ShouldSupportEquality()
        {
            // Arrange
            var music1 = Interests.Music;
            var music2 = Interests.Music;
            var travel = Interests.Travel;

            // Assert
            music1.Should().Be(music2);
            music1.Should().NotBe(travel);
        }

        [Fact]
        public void Interests_ShouldWorkInCollections()
        {
            // Arrange
            var interestsList = new List<Interests>
            {
                Interests.Music,
                Interests.Travel,
                Interests.Technology
            };

            // Act & Assert
            interestsList.Should().Contain(Interests.Music);
            interestsList.Should().Contain(Interests.Travel);
            interestsList.Should().Contain(Interests.Technology);
            interestsList.Should().HaveCount(3);
        }

        [Fact]
        public void Interests_ShouldWorkWithLinq()
        {
            // Arrange
            var allInterests = Enum.GetValues<Interests>();

            // Act
            var physicalInterests = allInterests.Where(i =>
                i == Interests.Sports ||
                i == Interests.Fitness ||
                i == Interests.Outdoors).ToList();

            var creativeInterests = allInterests.Where(i =>
                i == Interests.Music ||
                i == Interests.Arts ||
                i == Interests.Reading).ToList();

            // Assert
            physicalInterests.Should().HaveCount(3);
            physicalInterests.Should().Contain(Interests.Sports);
            physicalInterests.Should().Contain(Interests.Fitness);
            physicalInterests.Should().Contain(Interests.Outdoors);

            creativeInterests.Should().HaveCount(3);
            creativeInterests.Should().Contain(Interests.Music);
            creativeInterests.Should().Contain(Interests.Arts);
            creativeInterests.Should().Contain(Interests.Reading);
        }

        [Fact]
        public void Interests_ShouldBeUsableInHashSets()
        {
            // Arrange
            var interestsSet = new HashSet<Interests>
            {
                Interests.Gaming,
                Interests.Technology,
                Interests.Gaming // Duplicate should be ignored
            };

            // Assert
            interestsSet.Should().HaveCount(2);
            interestsSet.Should().Contain(Interests.Gaming);
            interestsSet.Should().Contain(Interests.Technology);
        }

        [Fact]
        public void Interests_ShouldSupportSwitchStatements()
        {
            // Arrange & Act
            var getCategory = (Interests interest) => interest switch
            {
                Interests.Sports or Interests.Fitness or Interests.Outdoors => "Physical",
                Interests.Music or Interests.Arts or Interests.Reading => "Creative",
                Interests.Technology or Interests.Gaming => "Digital",
                Interests.Travel or Interests.Food => "Lifestyle",
                _ => "Unknown"
            };

            // Assert
            getCategory(Interests.Sports).Should().Be("Physical");
            getCategory(Interests.Music).Should().Be("Creative");
            getCategory(Interests.Technology).Should().Be("Digital");
            getCategory(Interests.Travel).Should().Be("Lifestyle");
        }

        [Fact]
        public void Interests_AllValuesShouldBeDistinct()
        {
            // Arrange
            var allInterests = Enum.GetValues<Interests>();

            // Act
            var distinctInterests = allInterests.Distinct();

            // Assert
            distinctInterests.Should().HaveCount(allInterests.Length);
        }
    }
}
