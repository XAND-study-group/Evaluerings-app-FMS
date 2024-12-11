using School.Domain.Entities;
using School.Domain.Test.Fakes.Semester;
using Xunit;
using Assert = Xunit.Assert;

namespace School.Domain.Test.Tests.Semester;

public class ClassTests
{
    #region Tests

    #region CreationalTests

    [Theory]
    [InlineData("ValidName", "ValidDescription", 32)]
    public void Given_Valid_Data_Then_Class_Created(string name, string description, int studentCapacity)
    {
        // Act
        var classSut = Class.Create(name, description, studentCapacity, []);

        // Assert
        Assert.NotNull(classSut);
    }

    #endregion CreationalTests

    #region NameTests

    [Theory]
    [MemberData(nameof(NameNotUniqueData))]
    public void Given_Name_NotUnique_Then_Throws_ArgumentException(string name, IEnumerable<FakeClass> otherClasses)
    {
        // Arrange
        var classSut = new FakeClass();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.AssureNameIsUnique(name, otherClasses));
    }

    [Theory]
    [MemberData(nameof(NameUniqueData))]
    public void Given_Name_Unique_Then_Void(string name, IEnumerable<FakeClass> otherClasses)
    {
        // Arrange
        var classSut = new FakeClass();

        // Act
        classSut.AssureNameIsUnique(name, otherClasses);
    }

    #endregion NameTests

    #region CapacityTests

    [Fact]
    public void Given_Capacity_Below_One_Then_Throw_ArgumentException()
    {
        // Arrange
        var classSut = new FakeClass();
        var studentCapacity = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.SetStudentCapacity(studentCapacity));
    }

    [Fact]
    public void Given_StudentCount_Equal_To_StudentCapacity_Then_Throw_ArgumentException()
    {
        // Arrange
        var classSut = new FakeClass();
        var studentCount = 2;
        var studentCapacity = 2;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.AssureMaxCapacityIsNotReached(studentCount, studentCapacity));
    }

    [Fact]
    public void Given_Valid_StudentCount_Then_Void()
    {
        // Arrange
        var classSut = new FakeClass();
        var studentCount = 5;
        var studentCapacity = 10;

        // Act
        classSut.AssureMaxCapacityIsNotReached(studentCount, studentCapacity);
    }

    #endregion CapacityTests

    #region DescriptionTests

    [Fact]
    public void Given_Description_Equal_To_WhiteSpace_Then_Throw_ArgumentNullException()
    {
        // Arrange
        var classSut = new FakeClass();
        var description = " ";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.SetDescription(description));
    }

    [Fact]
    public void Given_Description_Equal_To_Null_Then_Throw_ArgumentNullException()
    {
        // Arrange
        var classSut = new FakeClass();
        string? description = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.SetDescription(description!));
    }

    [Fact]
    public void Given_Description_Length_Over_500_Then_Throw_ArgumentException()
    {
        // Arrange
        var classSut = new FakeClass();
        var description = string.Concat(Enumerable.Repeat("FakeTestNow", 50));

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.SetDescription(description));
    }

    [Fact]
    public void Given_Valid_Description_Then_Void()
    {
        // Arrange
        var classSut = new FakeClass();
        var description = "ValidDescription";

        // Act
        classSut.SetDescription(description);
    }

    #endregion DescriptionTests

    #endregion Tests

    #region MemberData Methods

    public static IEnumerable<object[]> ValidCreateData()
    {
        yield return new object[]
        {
        };
    }

    public static IEnumerable<object[]> NameNotUniqueData()
    {
        var otherClasses = OtherClassesNameNotUnique();
        yield return new object[]
        {
            "NotUnique",
            otherClasses
        };
    }

    public static IEnumerable<object[]> NameUniqueData()
    {
        var otherClasses = OtherClassesNameNotUnique();
        yield return new object[]
        {
            "Unique",
            otherClasses
        };
    }

    private static IEnumerable<FakeClass> OtherClassesNameNotUnique()
    {
        return new[] { new FakeClass("NotUnique") };
    }

    #endregion
}