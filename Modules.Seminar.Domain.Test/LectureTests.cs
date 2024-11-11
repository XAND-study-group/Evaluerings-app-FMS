using Module.Semester.Domain.Test.Fakes;
using Xunit;

namespace Module.Semester.Domain.Test;

public class LectureTests
{
    #region Tests

    #region LectureTitleTests

    [Fact]
    public void Given_WhiteSpace_Title_Then_Throw_ArgumentException()
    {
        // Arrange
        var lecture = new FakeLecture();
        var whiteSpaceTitle = " ";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => lecture.SetLectureTitle(whiteSpaceTitle));
    }

    [Fact]
    public void Given_Title_With_Length_101_Then_Throw_ArgumentException()
    {
        // Arrange
        var lecture = new FakeLecture();
        var tooLargeTitle = new string('x', 101);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => lecture.SetLectureTitle(tooLargeTitle));
    }

    [Fact]
    public void Given_Valid_Title_Then_Void()
    {
        // Arrange
        var lecture = new FakeLecture();
        var validTitle = "ValidTitle";

        // Act
        lecture.SetLectureTitle(validTitle);
    }

    #endregion LectureTitleTests

    #region TimePeriodTests

    [Fact]
    public void Given_StartTime_Greater_Than_EndTime_Then_Throw_ArgumentException()
    {
        // Arrange
        var lecture = new FakeLecture();
        var startTime = new TimeOnly(9, 30, 0);
        var endTime = new TimeOnly(8, 15, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => lecture.SetTimePeriod(startTime, endTime));
    }

    [Fact]
    public void Given_StartTime_Equal_To_EndTime_Then_Throw_ArgumentException()
    {
        // Arrange
        var lecture = new FakeLecture();
        var startTime = new TimeOnly(9, 30, 0);
        var endTime = new TimeOnly(9, 30, 0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => lecture.SetTimePeriod(startTime, endTime));
    }

    [Fact]
    public void Given_EndTime_One_Hour_After_StartTime_Then_Duration_Equal_To_One_Hour()
    {
        // Arrange
        var lecture = new FakeLecture();
        var startTime = new TimeOnly(9, 30, 0);
        var endTime = new TimeOnly(10, 30, 0);
        var expected = new TimeSpan(1, 0, 0);

        // Act
        lecture.SetTimePeriod(startTime, endTime);

        // Assert
        Assert.Equal(lecture.TimePeriod.Duration, expected);
    }

    [Fact]
    public void Given_Valid_TimeOnly_Then_Void()
    {
        // Arrange
        var lecture = new FakeLecture();
        var startTime = new TimeOnly(9, 30, 0);
        var endTime = new TimeOnly(10, 30, 0);

        // Act
        lecture.SetTimePeriod(startTime, endTime);
    }

    #endregion TimePeriodTests

    #region LectureDateTests

    [Fact]
    public void Given_StartDate_Earlier_Than_NowDate_Then_Throw_ArgumentException()
    {
        // Arrange
        var lecture = new FakeLecture();
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        // Act & Assert
        Assert.Throws<ArgumentException>(() => lecture.SetLectureDate(startDate));
    }

    [Fact]
    public void Given_Valid_StartDate_Then_Void()
    {
        // Arrange
        var lecture = new FakeLecture();
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        // Act
        lecture.SetLectureDate(startDate);
    }

    #endregion LectureDateTests

    #region ClassRoomTests

    [Fact]
    public void Given_WhiteSpace_ClassRoom_Then_Throw_ArgumentException()
    {
        // Arrange
        var lecture = new FakeLecture();
        var whiteSpaceClassRoom = " ";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => lecture.SetClassRoom(whiteSpaceClassRoom));
    }
    
    [Fact]
    public void Given_Valid_ClassRoom_Then_Void()
    {
        // Arrange
        var lecture = new FakeLecture();
        var classRoom = "ValidClassRoom";
        
        // Act
        lecture.SetClassRoom(classRoom);
    }
    #endregion ClassRoomTests

    #endregion Tests
}