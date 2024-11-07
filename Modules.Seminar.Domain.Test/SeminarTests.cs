using Module.Seminar.Domain.Test.Fakes;
using Xunit;

namespace Module.Seminar.Domain.Test;

public class SeminarTests
{
    #region Tests

    [Theory]
    [MemberData(nameof(ValidCreateData))]
    public void Given_Valid_Data_Then_Seminar_Created(string name, string description, DateOnly startDate,
        DateOnly endDate, int capacity)
    {
        // Act
        var seminar = Entity.Seminar.Create(name, description, startDate, endDate, capacity, []);

        // Assert
        Assert.NotNull(seminar);
    }

    [Theory]
    [MemberData(nameof(NameNotUniqueData))]
    public void Given_Name_NotUnique_Then_Throws_ArgumentException(string name, IEnumerable<FakeSeminar> otherSeminars)
    {
        // Arrange
        var seminar = new FakeSeminar(name);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => seminar.AssureNameIsUnique(name, otherSeminars));
    }

    [Theory]
    [MemberData(nameof(StartDateInPastData))]
    public void Given_StartDate_In_Past_Then_Throw_ArgumentException(DateOnly startDate, DateOnly nowDate)
    {
        // Arrange
        var seminar = new FakeSeminar("TestSeminar");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => seminar.AssureStartDateInFuture(startDate, nowDate));
    }

    [Theory]
    [MemberData(nameof(EndDateBeforeStartDateData))]
    public void Given_EndDate_Before_StartDate_Then_Throw_ArgumentException(DateOnly startDate, DateOnly endDate)
    {
        // Arrange
        var seminar = new FakeSeminar("TestSeminar");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => seminar.AssureEndDateAfterStartDate(startDate, endDate));
    }

    [Fact]
    public void Given_Capacity_Below_One_Then_Throw_ArgumentException()
    {
        // Arrange
        var seminar = new FakeSeminar("TestSeminar");
        var capacity = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => seminar.AssureCapacityIsAboveZero(capacity));
    }

    [Theory]
    [MemberData(nameof(MaxCapacityReachedData))]
    public void Given_StudentCount_Equal_To_StudentCapacity_Then_Throw_ArgumentException(int studentCount, int capacity)
    {
        // Arrange
        var seminar = new FakeSeminar("TestSeminar");
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => seminar.AssureMaxCapacityIsNotReached(studentCount, capacity));
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
        var otherSeminars = OtherSeminarsNameNotUnique();
        yield return new object[]
        {
            "NotUnique",
            otherSeminars
        };
    }

    private static IEnumerable<FakeSeminar> OtherSeminarsNameNotUnique()
        => new FakeSeminar[] { new FakeSeminar("NotUnique") };

    public static IEnumerable<object[]> StartDateInPastData()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        var nowDate = DateOnly.FromDateTime(DateTime.Now);

        yield return new object[]
        {
            startDate,
            nowDate
        };
    }

    public static IEnumerable<object[]> EndDateBeforeStartDateData()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        var endDate = DateOnly.FromDateTime(DateTime.Now);

        yield return new object[]
        {
            startDate,
            endDate
        };
    }

    public static IEnumerable<object[]> MaxCapacityReachedData()
    {
        var studentCount = 2;
        var capacity = 2;

        yield return new object[]
        {
            studentCount,
            capacity
        };
    }

    #endregion
}