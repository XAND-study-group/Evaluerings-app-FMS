using Module.Semester.Domain.Enums;
using SharedKernel.ValueObjects;

namespace Module.Semester.Domain.Entities;

public class Semester : Entity
{
    #region Properties
    // General Information
    public string Name { get; protected set; }
    public SemesterNumber SemesterNumber { get; protected set; }
    public EducationRange EducationRange { get; protected set; }
    public SchoolType School { get; protected set; }
    private readonly List<User> _semesterResponsibles = [];
    public IReadOnlyCollection<User> SemesterResponsibles => _semesterResponsibles;

    #endregion

    #region Constructors

    protected Semester()
    {
    }

    private Semester(string name, int semesterNumber, DateOnly startDate, DateOnly endDate,
        SchoolType school, IEnumerable<Semester> otherSemesters)
    {
        Name = name;
        SemesterNumber = semesterNumber;
        EducationRange = EducationRange.Create(startDate, endDate);
        School = school;
        
        AssureNameIsUnique(Name, otherSemesters);
    }

    #endregion

    #region Semester Methods

    public static Semester Create(string name, int semesterNumber, DateOnly startDate, DateOnly endDate,
        SchoolType school, IEnumerable<Semester> otherSemesters)
        => new Semester(name, semesterNumber, startDate, endDate, school, otherSemesters);

    #endregion

    #region Semester Business Logic Methods
    protected void AssureNameIsUnique(string name, IEnumerable<Semester> otherSemesterNames)
    {
        if (otherSemesterNames.Any(s => s.Name == name))
            throw new ArgumentException($"A Semester with name '{name}' already exists.");
    }
    #endregion
    
    #region Relational Methods

    public void AddResponsible(User teacher)
    {
        //TODO: Check User has Claim "Teacher"
        
        AssureNoDuplicateUser(teacher, _semesterResponsibles);

        _semesterResponsibles.Add(teacher);
    }

    #endregion
    
    #region Relational Business Logic Methods
    protected void AssureNoDuplicateUser(User teacher, List<User> semesterResponsibles)
    {
        if (semesterResponsibles.Any(u => u.Id == teacher.Id))
            throw new ArgumentException("This teacher has already been added to this Semester as one of its responsibles.");
    }
    #endregion
}