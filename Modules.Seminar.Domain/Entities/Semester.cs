using Module.Semester.Domain.Enums;

namespace Module.Semester.Domain.Entities;

public class Semester
{
    #region Properties

    // Database Properties
    public Guid Id { get; protected set; }
    public byte[] RowVersion { get; protected set; }

    // General Information
    public string Name { get; set; }
    public int SemesterNumber { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public SchoolType School { get; set; }
    private List<User> _SemesterResponsibles { get; set; } = [];

    public IEnumerable<User> SemesterResponsibles
    {
        get => _SemesterResponsibles;
        set => _SemesterResponsibles = value.ToList();
    }

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
        StartDate = startDate;
        EndDate = endDate;
        School = school;

        AssureStartDateInFuture(StartDate, DateOnly.FromDateTime(DateTime.Now));
        AssureEndDateAfterStartDate(StartDate, EndDate);
        AssureNameIsUnique(Name, otherSemesters);
        AssureSemesterNumberAboveZero(SemesterNumber);
    }

    #endregion

    #region Semester Methods

    public static Semester Create(string name, int semesterNumber, DateOnly startDate, DateOnly endDate,
        SchoolType school, IEnumerable<Semester> otherSemesters)
        => new Semester(name, semesterNumber, startDate, endDate, school, otherSemesters);

    #endregion

    #region Semester Business Logic Methods

    protected void AssureSemesterNumberAboveZero(int semesterNumber)
    {
        if (semesterNumber <= 0)
            throw new ArgumentException("Semester number cannot be less or equal to zero.");
    }

    protected void AssureEndDateAfterStartDate(DateOnly startDate, DateOnly endDate)
    {
        if (startDate >= endDate)
            throw new ArgumentException("End date has to be after the start date.");
    }

    protected void AssureStartDateInFuture(DateOnly startDate, DateOnly now)
    {
        if (startDate <= now)
            throw new ArgumentException("Start date has to be in the future.");
    }

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
        
        AssureNoDuplicateUser(teacher, _SemesterResponsibles);

        _SemesterResponsibles.Add(teacher);
    }

    #endregion
    
    #region Relational Business Logic Methods
    private void AssureNoDuplicateUser(User teacher, List<User> semesterResponsibles)
    {
        if (semesterResponsibles.Contains(teacher))
            throw new ArgumentException("This teacher has already been added to this Semester as one of its responsibles.");
    }
    #endregion
}