namespace Module.Semester.Domain.Entities;

public class Class : Entity
{
    #region Properties

    // List & IEnumberables
    private readonly List<User> _teachers = [];
    private List<User> _students { get; set; } = [];
    private List<Subject> _subjects { get; set; } = [];

    public IEnumerable<User> Teachers => _teachers;

    public IEnumerable<User> Students
    {
        get => _students;
        set => _students = value.ToList();
    }

    public IEnumerable<Subject> Subjects
    {
        get => _subjects;
        init => _subjects = value.ToList();
    }

    // General Information 
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int StudentCapacity { get; protected set; }

    #endregion

    #region Constructors

    protected Class()
    {
    }

    private Class(string name, string description, int studentCapacity, IEnumerable<Class> otherClassNames)
    {
        Name = name;
        Description = description;
        StudentCapacity = studentCapacity;

        AssureNameIsUnique(Name, otherClassNames);
        AssureCapacityIsAboveZero(StudentCapacity);
    }
    #endregion

    #region Class Methods
    public static Class Create(string name, string description, int capacity, IEnumerable<Class> otherClassNames)
        => new Class(name, description, capacity, otherClassNames);
    #endregion
    
    #region Class Business Logic Methods
    protected void AssureCapacityIsAboveZero(int capacity)
    {
        if (capacity <= 0) 
            throw new ArgumentException("Capacity must be greater than zero.");
    }

    protected void AssureNameIsUnique(string name, IEnumerable<Class> otherClassNames)
    {
        if (otherClassNames.Any(s => s.Name == name)) 
            throw new ArgumentException($"A Class with name '{name}' already exists.");
    }
    #endregion
    
    #region Relational Methods

    public void AddSubject()
    {
        /*
         var subject = Subject.Create();
        
        _subjects.Add(subject);
        */
    }

    public void AddStudent(User student)
    {
        // TODO: Check if user has CLAIM as Student
        AssureMaxCapacityIsNotReached(_students.Count, StudentCapacity);
        
        _students.Add(student);
    }

    public void AddTeacher(User teacher)
    {
        // TODO: Check if user has CLAIM as Teacher
        _teachers.Add(teacher);
    }
    #endregion
    
    #region Relation Business Logic Methods
    protected void AssureMaxCapacityIsNotReached(int studentsCount, int studentCapacity)
    {
        if (studentsCount >= studentCapacity) 
            throw new ArgumentException("Maximum number of students reached.");
    }
    #endregion
}