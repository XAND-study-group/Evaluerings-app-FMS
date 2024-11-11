using System.Collections;
using Module.Semester.Domain.Entities;
using Module.Semester.Domain.Enums;
using Module.Semester.Domain.Test.Fakes;
using Xunit;

namespace Module.Semester.Domain.Test;

public class SemesterTests
{
    #region Tests

    #region CreationalTests

    [Theory]
    [MemberData(nameof(CreateWithValidData))]
    public void Given_Valid_Data_Then_Create_Success(string name, int semesterNumber, DateOnly startDate,
        DateOnly endDate, SchoolType schoolType)
    {
        // Act
        var semester = Entities.Semester.Create(name, semesterNumber, startDate, endDate, schoolType, []);

        // Assert
        Assert.NotNull(semester);
    }

    #endregion CreationalTests

    #region EducationRangeTests

    [Theory]
    [MemberData(nameof(StartDateInPastData))]
    public void Given_StartDate_In_Past_Then_Throw_ArgumentException(DateOnly startDate, DateOnly endDate)
    {
        // Arrange
        var semester = new FakeSemester("TestSemester");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.SetEducationRange(startDate, endDate));
    }

    [Theory]
    [MemberData(nameof(EndDateBeforeStartDateData))]
    public void Given_EndDate_Before_StartDate_Then_Throw_ArgumentException(DateOnly startDate, DateOnly endDate)
    {
        // Arrange
        var semester = new FakeSemester("TestSemester");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.SetEducationRange(startDate, endDate));
    }

    [Fact]
    public void Given_Valid_Dates_Then_Void()
    {
        // Arrange
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));
        var endDate = DateOnly.FromDateTime(DateTime.Now.AddYears(3));
        var semester = new FakeSemester("TestSemester");

        // Act
        semester.SetEducationRange(startDate, endDate);
    }

    #endregion EducationRangeTests

    #region SemesterNumberTests

    [Fact]
    public void Given_Negative_SemesterNumber_Then_Throw_ArgumentException()
    {
        // Arrange
        var semester = new FakeSemester("TestSemester");
        var semesterNumber = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.SetSemesterNumber(semesterNumber));
    }

    [Fact]
    public void Given_SemesterNumber_Above_12_Then_Throw_ArgumentException()
    {
        // Arrange
        var semester = new FakeSemester("TestSemester");
        var semesterNumber = 13;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.SetSemesterNumber(semesterNumber));
    }

    [Fact]
    public void Given_Valid_SemesterNumber_Then_Void()
    {
        // Arrange
        var semester = new FakeSemester("TestSemester");
        var semesterNumber = 6;

        // Act
        semester.SetSemesterNumber(semesterNumber);
    }

    #endregion SemesterNumberTests

    #region NameTests

    [Theory]
    [MemberData(nameof(NotUniqueNameData))]
    public void Given_NotUniqueName_Then_Throw_ArgumentException(string name, IEnumerable<FakeSemester> otherSemesters)
    {
        // Arrange
        var semester = new FakeSemester(name);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.AssureNameIsUnique(semester.Name, otherSemesters));
    }

    [Theory]
    [MemberData(nameof(UniqueNameData))]
    public void Given_UniqueName_Then_Void(string name, IEnumerable<FakeSemester> otherSemesters)
    {
        // Arrange
        var semester = new FakeSemester(name);

        // Act
        semester.AssureNameIsUnique(name, otherSemesters);
    }

    #endregion NameTests

    #region AddResponsiblesTests

    [Theory]
    [MemberData(nameof(ValidResponsibleData))]
    public void Given_Unique_User_Then_Responsible_Count_Increased(FakeUser teacher)
    {
        // Arrange
        var semester = new FakeSemester("TestSemester");
        
        // Act
        semester.AddResponsible(teacher);
        
        // Assert
        Assert.True(semester.SemesterResponsibles.Count == 1);
    }
    
    [Theory]
    [MemberData(nameof(DuplicateResponsibleData))]
    public void Given_Duplicate_User_Then_Throw_ArgumentException(FakeUser teacher, IEnumerable<User> otherResponsibles)
    {
        // Arrange
        var semester = new FakeSemester("TestSemester");
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.AssureNoDuplicateUser(teacher, otherResponsibles.ToList()));
    }

    #endregion AddResponsiblesTests

    #endregion Tests

    #region Member Data Methods

    public static IEnumerable<object[]> CreateWithValidData()
    {
        yield return new object[]
        {
            "Test Semester",
            3,
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
            SchoolType.Fredericia
        };
    }

    public static IEnumerable<object[]> StartDateInPastData()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        yield return new object[]
        {
            startDate,
            endDate
        };
    }

    public static IEnumerable<object[]> EndDateBeforeStartDateData()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
        var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        yield return new object[]
        {
            startDate,
            endDate
        };
    }

    public static IEnumerable<object[]> NotUniqueNameData()
    {
        var otherSemesters = GetOtherSemesters();
        yield return new object[]
        {
            "NotUniqueName",
            otherSemesters
        };
    }

    public static IEnumerable<object[]> UniqueNameData()
    {
        var otherSemesters = GetOtherSemesters();
        yield return new object[]
        {
            "VeryUniqueName",
            otherSemesters
        };
    }

    private static IEnumerable<FakeSemester> GetOtherSemesters()
        => new[]
        {
            new FakeSemester("NotUniqueName"),
            new FakeSemester("UniqueName"),
        };

    public static IEnumerable<object[]> ValidResponsibleData()
    {
        yield return new object[]
        {
            new FakeUser(new Guid())
        };
    }
    public static IEnumerable<object[]> DuplicateResponsibleData()
    {
        var otherResponsibles = GetResponsibles();
        yield return new object[]
        {
            new FakeUser(Guid.Parse("b2d214ea-c5ea-419c-8498-0a023c27f514")),
            otherResponsibles
        };
    }

    private static IEnumerable<FakeUser> GetResponsibles()
        => new[]
        {
            new FakeUser(Guid.Parse("b2d214ea-c5ea-419c-8498-0a023c27f514")),
            new FakeUser(Guid.Parse("99bf6d44-620a-4820-829d-a9444590e1d5")),
        };

    #endregion
}