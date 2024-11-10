using Module.Class.Domain.Test.Fakes;
using Xunit;

namespace Module.Semester.Domain.Test;

public class SemesterTests
{
    [Theory]
    [MemberData(nameof(StartDateInPastData))]
    public void Given_StartDate_In_Past_Then_Throw_ArgumentException(DateOnly startDate, DateOnly nowDate)
    {
        // Arrange
        var classSut = new FakeSemester("TestSemester");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.AssureStartDateInFuture(startDate, nowDate));
    }

    [Theory]
    [MemberData(nameof(EndDateBeforeStartDateData))]
    public void Given_EndDate_Before_StartDate_Then_Throw_ArgumentException(DateOnly startDate, DateOnly endDate)
    {
        // Arrange
        var classSut = new FakeSemester("TestSemester");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => classSut.AssureEndDateAfterStartDate(startDate, endDate));
    }

    #region Member Data Methods
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
    #endregion
}