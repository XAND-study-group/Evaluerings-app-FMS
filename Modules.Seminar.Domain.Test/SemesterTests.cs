using Module.Semester.Domain.Entities;
using Module.Semester.Domain.Test.Fakes;
using SharedKernel.Enums.Features.Semester;
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

    [Fact]
    public void Given_StartDate_In_Past_Then_Throw_ArgumentException()
    {
        // Arrange
        var semester = new FakeSemester();
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.SetEducationRange(startDate, endDate));
    }

    [Fact]
    public void Given_EndDate_Before_StartDate_Then_Throw_ArgumentException()
    {
        // Arrange
        var semester = new FakeSemester();
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
        var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.SetEducationRange(startDate, endDate));
    }

    [Fact]
    public void Given_Valid_Dates_Then_Void()
    {
        // Arrange
        var semester = new FakeSemester();
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));
        var endDate = DateOnly.FromDateTime(DateTime.Now.AddYears(3));

        // Act
        semester.SetEducationRange(startDate, endDate);
    }

    #endregion EducationRangeTests

    #region SemesterNumberTests

    [Fact]
    public void Given_Negative_SemesterNumber_Then_Throw_ArgumentException()
    {
        // Arrange
        var semester = new FakeSemester();
        var semesterNumber = -1;
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.SetSemesterNumber(semesterNumber));
    }

    [Fact]
    public void Given_SemesterNumber_Above_12_Then_Throw_ArgumentException()
    {
        // Arrange
        var semester = new FakeSemester();
        var semesterNumber = 13;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.SetSemesterNumber(semesterNumber));
    }

    [Fact]
    public void Given_Valid_SemesterNumber_Then_Void()
    {
        // Arrange
        var semester = new FakeSemester();
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
        var semester = new FakeSemester();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.AssureNameIsUnique(name, otherSemesters));
    }

    [Theory]
    [MemberData(nameof(UniqueNameData))]
    public void Given_UniqueName_Then_Void(string name, IEnumerable<FakeSemester> otherSemesters)
    {
        // Arrange
        var semester = new FakeSemester();

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
        var semester = new FakeSemester();
        var expected = 1;

        // Act
        semester.AddResponsible(teacher);

        // Assert
        Assert.Equal(semester.SemesterResponsibles.Count, expected);
    }

    [Theory]
    [MemberData(nameof(DuplicateResponsibleData))]
    public void Given_Duplicate_User_Then_Throw_ArgumentException(FakeUser teacher, IEnumerable<User> otherResponsibles)
    {
        // Arrange
        var semester = new FakeSemester();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.AssureNoDuplicateUser(teacher, otherResponsibles.ToList()));
    }

    #endregion AddResponsiblesTests

    #region AddClassTests

    [Theory]
    [InlineData("ValidName", "ValidDescription", 20)]
    public void Given_Valid_Class_Then_List_Count_Increased(string name, string description, int studentCapacity)
    {
        // Arrange
        var semester = new FakeSemester();
        IEnumerable<Class> otherClasses = [];
        var expected = 1;

        // Act
        semester.AddClass(name, description, studentCapacity, otherClasses);

        // Assert
        Assert.Equal(semester.Classes.Count, expected);
    }

    [Theory]
    [MemberData(nameof(DuplicateClassData))]
    public void Given_Duplicate_Class_Then_Throw_ArgumentException(FakeClass fakeClass, IEnumerable<Class> otherClasses)
    {
        // Arrange
        var semester = new FakeSemester();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => semester.AssureNoDuplicateClass(fakeClass, otherClasses.ToList()));
    }

    #endregion AddClassTests

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

    public static IEnumerable<object[]> DuplicateClassData()
    {
        var otherClasses = GetOtherClasses();
        yield return new object[]
        {
            new FakeClass(Guid.Parse("80d1ffdd-b31d-4183-bcc6-6709e7177de7"), "TestClass"),
            otherClasses
        };
    }

    private static IEnumerable<FakeClass> GetOtherClasses()
        =>
        [
            new FakeClass(Guid.Parse("80d1ffdd-b31d-4183-bcc6-6709e7177de7"), "TestClass"),
            new FakeClass(Guid.Parse("501b49fd-9428-456e-8fe7-95c24bbc8a88"), "OtherTestClass")
        ];

    #endregion
}