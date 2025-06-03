namespace MatchDotCom.UserProfile.Tests
{
    public class GenderOptionsTests
    {
        [Fact]
        public void GenderOptions_ShouldHaveThreeValues()
        {
            // Arrange
            var expectedValues = new[] { GenderOptions.Male, GenderOptions.Female, GenderOptions.Other };

            // Act
            var actualValues = Enum.GetValues<GenderOptions>();

            // Assert
            actualValues.Should().HaveCount(3);
            actualValues.Should().BeEquivalentTo(expectedValues);
        }

        [Theory]
        [InlineData(GenderOptions.Male, "Male")]
        [InlineData(GenderOptions.Female, "Female")]
        [InlineData(GenderOptions.Other, "Other")]
        public void GenderOptions_ShouldHaveCorrectStringRepresentation(GenderOptions gender, string expectedString)
        {
            // Act
            var stringValue = gender.ToString();

            // Assert
            stringValue.Should().Be(expectedString);
        }

        [Fact]
        public void GenderOptions_ShouldBeConvertibleToInt()
        {
            // Act & Assert
            ((int)GenderOptions.Male).Should().Be(0);
            ((int)GenderOptions.Female).Should().Be(1);
            ((int)GenderOptions.Other).Should().Be(2);
        }

        [Fact]
        public void GenderOptions_ShouldBeConvertibleFromInt()
        {
            // Act & Assert
            ((GenderOptions)0).Should().Be(GenderOptions.Male);
            ((GenderOptions)1).Should().Be(GenderOptions.Female);
            ((GenderOptions)2).Should().Be(GenderOptions.Other);
        }

        [Fact]
        public void GenderOptions_ShouldSupportEquality()
        {
            // Arrange
            var male1 = GenderOptions.Male;
            var male2 = GenderOptions.Male;
            var female = GenderOptions.Female;

            // Assert
            male1.Should().Be(male2);
            male1.Should().NotBe(female);
        }

        [Fact]
        public void GenderOptions_ShouldWorkInCollections()
        {
            // Arrange
            var genderList = new List<GenderOptions>
            {
                GenderOptions.Male,
                GenderOptions.Female,
                GenderOptions.Other
            };

            // Act & Assert
            genderList.Should().Contain(GenderOptions.Male);
            genderList.Should().Contain(GenderOptions.Female);
            genderList.Should().Contain(GenderOptions.Other);
            genderList.Should().HaveCount(3);
        }

        [Fact]
        public void GenderOptions_ShouldWorkWithLinq()
        {
            // Arrange
            var allGenders = Enum.GetValues<GenderOptions>();

            // Act
            var maleGenders = allGenders.Where(g => g == GenderOptions.Male).ToList();
            var nonMaleGenders = allGenders.Where(g => g != GenderOptions.Male).ToList();

            // Assert
            maleGenders.Should().HaveCount(1);
            maleGenders.Should().Contain(GenderOptions.Male);
            nonMaleGenders.Should().HaveCount(2);
            nonMaleGenders.Should().Contain(GenderOptions.Female);
            nonMaleGenders.Should().Contain(GenderOptions.Other);
        }
    }
}
