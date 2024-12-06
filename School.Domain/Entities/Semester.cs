using School.Domain.ValueObjects;
using SharedKernel.Enums.Features.Semester;
using SharedKernel.Models;

namespace School.Domain.Entities;

public class Semester : Entity
{
    #region Properties

    // General Information
    public SemesterName Name { get; protected set; }
    public SemesterNumber SemesterNumber { get; protected set; }
    public EducationRange EducationRange { get; protected set; }
    public SchoolType School { get; protected set; }

    private readonly List<User> _semesterResponsibles = [];
    private readonly List<Class> _classes = [];

    public IReadOnlyCollection<User> SemesterResponsibles => _semesterResponsibles;
    public IReadOnlyCollection<Class> Classes => _classes;

    #endregion

    #region Constructors

    protected Semester()
    {
    }

    private Semester(string name, int semesterNumber, DateOnly startDate, DateOnly endDate,
        SchoolType school, IEnumerable<Semester> otherSemesters)
    {
        var otherSemesterNames = otherSemesters.Select(s => s.Name.Value);

        Name = SemesterName.Create(name, otherSemesterNames);
        SemesterNumber = semesterNumber;
        EducationRange = EducationRange.Create(startDate, endDate);
        School = school;
    }

    private Semester(string name, int semesterNumber, DateOnly startDate, DateOnly endDate,
        SchoolType school, IEnumerable<Class> classes, IEnumerable<Semester> otherSemesters)
    {
        var otherSemesterNames = otherSemesters.Select(s => s.Name.Value);

        Name = SemesterName.Create(name, otherSemesterNames);
        SemesterNumber = semesterNumber;
        EducationRange = EducationRange.Create(startDate, endDate);
        School = school;
        _classes = classes.ToList();
    }

    #endregion

    #region Semester Methods

    public static Semester Create(string name, int semesterNumber, DateOnly startDate, DateOnly endDate,
        SchoolType school, IEnumerable<Semester> otherSemesters)
    {
        return new Semester(name, semesterNumber, startDate, endDate, school, otherSemesters);
    }

    public static Semester Create(string name, int semesterNumber, DateOnly startDate, DateOnly endDate,
        SchoolType school, IEnumerable<Class> classes, IEnumerable<Semester> otherSemesters)
    {
        return new Semester(name, semesterNumber, startDate, endDate, school, classes, otherSemesters);
    }

    #endregion

    #region Relational Methods

    public void AddResponsible(User teacher)
    {
        AssureNoDuplicateUser(teacher, _semesterResponsibles);
        AssureCorrectRole("Teacher", teacher);

        _semesterResponsibles.Add(teacher);
    }

    public Class AddClass(string name,
        string description,
        int studentCapacity,
        IEnumerable<Class> otherClasses)
    {
        var classToCreate = Class.Create(name, description, studentCapacity, otherClasses);

        AssureNoDuplicateClass(classToCreate, _classes);

        _classes.Add(classToCreate);

        return classToCreate;
    }

    public Lecture AddLectureToClass(string lectureTitle, string description, TimeOnly startTime,
        TimeOnly endTime, DateOnly date, string classRoom, Guid classId,
        Guid subjectId)
    {
        var classToFind = _classes.Single(c => c.Id == classId);
        return classToFind.AddLectureToSubject(lectureTitle, description, startTime, endTime, date, classRoom,
            subjectId);
    }

    #endregion

    #region Relational Business Logic Methods

    protected void AssureNoDuplicateUser(User teacher, List<User> semesterResponsibles)
    {
        if (semesterResponsibles.Any(u => u.Id == teacher.Id))
            throw new ArgumentException(
                "This teacher has already been added to this Semester as one of its responsibles.");
    }

    protected void AssureNoDuplicateClass(Class classToCreate, List<Class> classes)
    {
        if (classes.Any(c => c.Id == classToCreate.Id))
            throw new ArgumentException("This class has already been added to this Semester.");
    }

    protected void AssureCorrectRole(string roleValueName, User user)
    {
        if (user.AccountClaims.All(c => c.ClaimValue != roleValueName))
            throw new ArgumentException("Brugeren har ikke den korrekte rolle");
    }

    #endregion
}