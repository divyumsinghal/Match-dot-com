namespace MatchDotCom.UserProfile.Tests
{
    public class MatchCriteriaTests
    {
        [Fact]
        public void MatchCriteria_WithValidParameters_ShouldCreateSuccessfully()
        {
            // Arrange
            var ageRange = 5;
            var commonInterests = new List<Interests> { Interests.Music, Interests.Travel };
            var distance = 25.5;

            // Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = ageRange,
                InterestsCommon = commonInterests,
                Distance = distance
            };

            // Assert
            matchCriteria.AgeRange.Should().Be(ageRange);
            matchCriteria.InterestsCommon.Should().BeEquivalentTo(commonInterests);
            matchCriteria.Distance.Should().Be(distance);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(20)]
        public void MatchCriteria_WithVariousAgeRanges_ShouldCreateSuccessfully(int ageRange)
        {
            // Arrange
            var commonInterests = new List<Interests> { Interests.Reading };

            // Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = ageRange,
                InterestsCommon = commonInterests,
                Distance = 10.0
            };

            // Assert
            matchCriteria.AgeRange.Should().Be(ageRange);
        }

        [Theory]
        [InlineData(0.5)]
        [InlineData(1.0)]
        [InlineData(10.5)]
        [InlineData(25.0)]
        [InlineData(50.0)]
        [InlineData(100.0)]
        public void MatchCriteria_WithVariousDistances_ShouldCreateSuccessfully(double distance)
        {
            // Arrange
            var commonInterests = new List<Interests> { Interests.Sports };

            // Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = 5,
                InterestsCommon = commonInterests,
                Distance = distance
            };

            // Assert
            matchCriteria.Distance.Should().Be(distance);
        }

        [Fact]
        public void MatchCriteria_WithSingleInterest_ShouldCreateSuccessfully()
        {
            // Arrange
            var singleInterest = new List<Interests> { Interests.Technology };

            // Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = 3,
                InterestsCommon = singleInterest,
                Distance = 15.0
            };

            // Assert
            matchCriteria.InterestsCommon.Should().HaveCount(1);
            matchCriteria.InterestsCommon.Should().Contain(Interests.Technology);
        }

        [Fact]
        public void MatchCriteria_WithMultipleInterests_ShouldCreateSuccessfully()
        {
            // Arrange
            var multipleInterests = new List<Interests>
            {
                Interests.Music,
                Interests.Travel,
                Interests.Food,
                Interests.Arts
            };

            // Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = 7,
                InterestsCommon = multipleInterests,
                Distance = 30.0
            };

            // Assert
            matchCriteria.InterestsCommon.Should().HaveCount(4);
            matchCriteria.InterestsCommon.Should().BeEquivalentTo(multipleInterests);
        }

        [Fact]
        public void MatchCriteria_WithEmptyInterestsList_ShouldCreateSuccessfully()
        {
            // Arrange
            var emptyInterests = new List<Interests>();

            // Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = 2,
                InterestsCommon = emptyInterests,
                Distance = 5.0
            };

            // Assert
            matchCriteria.InterestsCommon.Should().BeEmpty();
        }

        [Fact]
        public void MatchCriteria_WithZeroValues_ShouldCreateSuccessfully()
        {
            // Arrange
            var interests = new List<Interests> { Interests.Gaming };

            // Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = 0,
                InterestsCommon = interests,
                Distance = 0.0
            };

            // Assert
            matchCriteria.AgeRange.Should().Be(0);
            matchCriteria.Distance.Should().Be(0.0);
        }

        [Fact]
        public void MatchCriteria_WithNegativeAgeRange_ShouldStillCreate()
        {
            // Note: Testing current behavior - no validation exists
            // Arrange
            var interests = new List<Interests> { Interests.Fitness };

            // Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = -5,
                InterestsCommon = interests,
                Distance = 10.0
            };

            // Assert
            matchCriteria.AgeRange.Should().Be(-5);
        }

        [Fact]
        public void MatchCriteria_WithNegativeDistance_ShouldStillCreate()
        {
            // Note: Testing current behavior - no validation exists
            // Arrange
            var interests = new List<Interests> { Interests.Outdoors };

            // Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = 5,
                InterestsCommon = interests,
                Distance = -10.0
            };

            // Assert
            matchCriteria.Distance.Should().Be(-10.0);
        }

        [Fact]
        public void MatchCriteria_AllProperties_ShouldBeSettable()
        {
            // Arrange & Act
            var matchCriteria = new MatchDotCom.UserMatching.MatchCriteria
            {
                AgeRange = 8,
                InterestsCommon = new List<Interests> { Interests.Music, Interests.Arts },
                Distance = 20.5
            };

            // Modify properties
            matchCriteria.AgeRange = 12;
            matchCriteria.Distance = 35.0;
            matchCriteria.InterestsCommon.Add(Interests.Travel);

            // Assert
            matchCriteria.AgeRange.Should().Be(12);
            matchCriteria.Distance.Should().Be(35.0);
            matchCriteria.InterestsCommon.Should().HaveCount(3);
            matchCriteria.InterestsCommon.Should().Contain(Interests.Travel);
        }
    }
}
