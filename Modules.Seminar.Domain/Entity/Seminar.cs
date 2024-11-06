namespace Module.Seminar.Domain.Entity;

public class Seminar
{
    #region Properties

    // Database Properties
    public Guid Id { get; protected set; }
    public byte[] RowVersion { get; protected set; }

    // List & IEnumberables
    private List<User> _teachers { get; set; } = [];
    private List<User> _students { get; set; } = [];
    private List<Subject> _subjects { get; set; } = [];

    public IEnumerable<User> Teachers
    {
        get => _teachers;
        private set => _teachers = value.ToList();
    }

    public IEnumerable<User> Students
    {
        get => _students;
        private set => _students = value.ToList();
    }

    public IEnumerable<Subject> Subjects
    {
        get => _subjects;
        set => _subjects = value.ToList();
    }

    // General Information 
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public DateTime StartDate { get; protected set; }
    public DateTime EndDate { get; protected set; }
    public int Capacity { get; protected set; }

    #endregion

    #region Constructors

    protected Seminar()
    {
    }

    private Seminar(string name, string description, DateTime startDate, DateTime endDate, int capacity,
        IEnumerable<Seminar> otherSeminarNames)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Capacity = capacity;

        AssureNameIsUnique(Name, otherSeminarNames);
        AssureStartDateInFuture(StartDate, DateTime.Now);
        AssureEndDateAfterStartDate(StartDate, EndDate);
        AssureCapacityIsAboveZero(Capacity);
    }
    #endregion

    #region Seminar Methods
    public static Seminar Create(string name, string description, DateTime startDate, DateTime endDate, int capacity,
        IEnumerable<Seminar> otherSeminarNames)
        => new Seminar(name, description, startDate, endDate, capacity, otherSeminarNames);
    #endregion
    
    #region Seminar Business Logic Methods
    protected void AssureCapacityIsAboveZero(int capacity)
    {
        if (capacity <= 0) 
            throw new ArgumentException("Capacity must be greater than zero.");
    }

    protected void AssureEndDateAfterStartDate(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate) 
            throw new ArgumentException("End date has to be after the start date.");
    }

    protected void AssureStartDateInFuture(DateTime startDate, DateTime now)
    {
        if (startDate <= DateTime.Now) 
            throw new ArgumentException("Start date has to be in the future."); 
    }

    protected void AssureNameIsUnique(string name, IEnumerable<Seminar> otherSeminarNames)
    {
        if (otherSeminarNames.Any(s => s.Name == name)) 
            throw new ArgumentException($"A Seminar with name '{name}' already exists.");
    }
    #endregion
    
    #region Relational Methods

    public void AddSubject()
    {
        var subject = Subject.Create();
        
        _subjects.Add(subject);
    }

    public void AddStudent(User student)
    {
        AssureMaxCapacityIsNotReached(_students.Count, Capacity);
        
        _students.Add(student);
    }

    public void AddTeacher(User teacher)
    {
        _teachers.Add(teacher);
    }
    #endregion
    
    #region Relation Business Logic Methods
    private void AssureMaxCapacityIsNotReached(int studentsCount, int capacity)
    {
        if (studentsCount >= capacity) 
            throw new ArgumentException("Maximum number of students reached.");
    }
    #endregion
}