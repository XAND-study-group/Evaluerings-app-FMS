using School.Domain.Entities;
using School.Domain.Test.Fakes.Semester;
using Xunit;

namespace School.Domain.Test.Tests.Semester;

public class LectureTests
{
    #region Tests
    
    #region CreationalTests

    [Theory]
    [MemberData(nameof(ValidCreateData))]
    public void Given_Valid_Data_Then_Success(string lectureTitle, string description, TimeOnly start, TimeOnly end,
        DateOnly date, string classRoom)
    {
        // Act
        var lecture = Lecture.Create(lectureTitle, description, start, end, date, classRoom);
        
        // Assert
        Assert.NotNull(lecture);
    }
    #endregion CreationalTests

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
    
    #region TeachersListTests

    [Theory]
    [MemberData(nameof(UniqueTeacherData))]
    public void Given_Unique_Teacher_Then_List_Count_Increased(FakeUser teacher)
    {
        // Arrange
        var lecture = new FakeLecture();
        var expected = 1;
        
        // Act
        lecture.AddTeacher(teacher);
        
        // Assert
        Assert.Equal(expected, lecture.Teachers.Count);
    }

    [Theory]
    [MemberData(nameof(NotUniqueTeacherData))]
    public void Given_Not_Unique_Teacher_Then_Throw_InvalidOperationException(FakeUser teacher, IEnumerable<Entities.User> teachers)
    {
        // Arrange
        var lecture = new FakeLecture();
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => lecture.AssureNoDuplicates(teacher, teachers.ToList()));
    }
    #endregion TeachersListTests

    #endregion Tests
    
    #region MemberData Methods

    public static IEnumerable<object[]> ValidCreateData()
    {
        yield return new object[]
        {
            "ValidTitle",
            "ValidDescription",
            new TimeOnly(9, 30, 0),
            new TimeOnly(10, 30, 0),
            DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
            "ValidClassRoom"
        };
    }

    public static IEnumerable<object[]> UniqueTeacherData()
    {
        yield return new object[]
        {
            new FakeUser(new Guid())
        };
    }

    public static IEnumerable<object[]> NotUniqueTeacherData()
    {
        var currentTeachers = GetCurrentTeachers();
        yield return new object[]
        {
            new FakeUser(Guid.Parse("6e70b105-2bce-42c9-b9df-c89f1617a9a2")),
            currentTeachers
        };
    }

    private static IEnumerable<Entities.User> GetCurrentTeachers()
        =>
        [
            new FakeUser(Guid.Parse("6e70b105-2bce-42c9-b9df-c89f1617a9a2")),
            new FakeUser(Guid.Parse("512b72cf-dc45-4d68-9f9d-68ea0d6d35f2"))
        ];

    #endregion MemberData Methods
}