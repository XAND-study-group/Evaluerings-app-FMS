using School.Domain.ValueObjects;
using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace School.Domain.Entities;

public class Lecture : Entity
{
    #region Properties

    public Title Title { get; protected set; }
    public Text Description { get; protected set; }
    public TimePeriod TimePeriod { get; protected set; }
    public LectureDate LectureDate { get; protected set; }
    public ClassRoom ClassRoom { get; protected set; }
    private readonly List<User> _teachers = [];
    public IReadOnlyCollection<User> Teachers => _teachers;
    

    #endregion Properties

    #region Constructors

    protected Lecture()
    {
    }

    private Lecture(string title, string description, TimeOnly startTime, TimeOnly endTime, DateOnly date,
        string classRoom)
    {
        Title = title;
        Description = description;
        TimePeriod = new TimePeriod(startTime, endTime);
        LectureDate = date;
        ClassRoom = classRoom;
    }

    #endregion Constructors

    #region Lecture Methods

    public static Lecture Create(string lectureTitle, string description, TimeOnly startTime, TimeOnly endTime,
        DateOnly date, string classRoom)
        => new Lecture(lectureTitle, description, startTime, endTime, date, classRoom);

    #endregion Lecture Methods
    
    #region Relational Methods

    public void AddTeacher(User user)
    {
        AssureCorrectRole("Teacher", user);
        AssureNoDuplicates(user, _teachers);
        
        _teachers.Add(user);
    }

    protected void AssureNoDuplicates(User user, List<User> teachersList)
    {
        if (teachersList.Any(t => t.Id == user.Id))
            throw new InvalidOperationException("User already exists in the teachers list.");
    }
    
    protected void AssureCorrectRole(string roleValueName, User user)
    {
        if(user.AccountClaims.All(c => c.ClaimValue != roleValueName))
            throw new ArgumentException($"Brugeren har ikke den korrekte rolle");

    }

    #endregion Relational Methods
}