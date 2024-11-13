using SharedKernel.ValueObjects;

namespace Module.Semester.Domain.Entities;

public class Class : Entity
{
    #region Properties

    // List & IReadOnlyCollections
    private readonly List<User> _teachers = [];
    private readonly List<User> _students = [];
    private readonly List<Subject> _subjects = [];
    public IReadOnlyCollection<User> Teachers => _teachers;
    public IReadOnlyCollection<User> Students => _students;
    public IReadOnlyCollection<Subject> Subjects => _subjects;

    // General Information 
    public string Name { get; protected set; }
    public Text Description { get; protected set; }
    public StudentCapacity StudentCapacity { get; protected set; }

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
    }
    #endregion

    #region Class Methods
    public static Class Create(string name, string description, int capacity, IEnumerable<Class> otherClassNames)
        => new Class(name, description, capacity, otherClassNames);
    #endregion
    
    #region Class Business Logic Methods
    protected void AssureNameIsUnique(string name, IEnumerable<Class> otherClassNames)
    {
        if (otherClassNames.Any(s => s.Name == name)) 
            throw new ArgumentException($"A Class with name '{name}' already exists.");
    }
    #endregion

    #region Relational Methods

    public void AddSubject(Subject subject)
    {
        _subjects.Add(subject);
    }

    public void AddStudent(User student)
    {
        // TODO: Check if user has CLAIM as Student
        AssureMaxCapacityIsNotReached(_students.Count, StudentCapacity.Value);
        
        _students.Add(student);
    }

    public void AddTeacher(User teacher)
    {
        // TODO: Check if user has CLAIM as Teacher
        _teachers.Add(teacher);
    }

    public Lecture AddLectureToSubject(string lectureTitle, string description, TimeOnly startTime, TimeOnly endTime,
        DateOnly date, string classRoom, Guid subjectId)
    {
        var subject = _subjects.Single(s => s.Id == subjectId);
        
        return subject.AddLecture(lectureTitle, description, startTime, endTime, date, classRoom);
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