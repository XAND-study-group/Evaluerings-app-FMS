using Module.Semester.Domain.Test.Fakes;
using Xunit;

namespace Module.Semester.Domain.Test;

public class ClassTests
{
    #region Tests

    [Theory]
    [MemberData(nameof(ValidCreateData))]
    public void Given_Valid_Data_Then_Class_Created(string name, string description, int studentCapacity)
    {
        // Act
        var classSut = Entities.Class.Create(name, description, studentCapacity, []);

        // Assert
        Assert.NotNull(classSut);
    }

    [Theory]
    [MemberData(nameof(NameNotUniqueData))]
    public void Given_Name_NotUnique_Then_Throws_ArgumentException(string name, IEnumerable<FakeClass> otherClasses)
    {
        // Arrange
        var classSut = new FakeClass(name);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.AssureNameIsUnique(name, otherClasses));
    }

    [Fact]
    public void Given_Capacity_Below_One_Then_Throw_ArgumentException()
    {
        // Arrange
        var classSut = new FakeClass("TestClass");
        var studentCapacity = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.AssureCapacityIsAboveZero(studentCapacity));
    }

    [Theory]
    [MemberData(nameof(MaxCapacityReachedData))]
    public void Given_StudentCount_Equal_To_StudentCapacity_Then_Throw_ArgumentException(int studentCount, int studentCapacity)
    {
        // Arrange
        var classSut = new FakeClass("TestClass");
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.AssureMaxCapacityIsNotReached(studentCount, studentCapacity));
    }
    #endregion

    #region MemberData Methods

    public static IEnumerable<object[]> ValidCreateData()
    {
        yield return new object[]
        {
            "DMVE231",
            "Empty Description",
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            32
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

    private static IEnumerable<FakeClass> OtherClassesNameNotUnique()
        => new FakeClass[] { new FakeClass("NotUnique") };
    

    public static IEnumerable<object[]> MaxCapacityReachedData()
    {
        var studentCount = 2;
        var studentCapacity = 2;

        yield return new object[]
        {
            studentCount,
            studentCapacity
        };
    }

    #endregion
}