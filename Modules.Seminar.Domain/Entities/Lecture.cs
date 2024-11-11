using Module.Semester.Domain.ValueObjects;

namespace Module.Semester.Domain.Entities;

public class Lecture : Entity
{
    #region Properties
    public LectureTitle LectureTitle { get; protected set; }
    public Text Description { get; protected set; }
    public TimePeriod TimePeriod { get; protected set; }
    public LectureDate LectureDate { get; protected set; }
    public ClassRoom ClassRoom { get; protected set; }
    private readonly List<User> _teachers = [];
    public IReadOnlyCollection<User> Teachers => _teachers;
    #endregion Properties
    
    #region Constructors
    protected Lecture(){}

    private Lecture(string lectureTitle, string description, TimeOnly startTime, TimeOnly endTime, DateOnly date, string classRoom)
    {
        LectureTitle = lectureTitle;
        Description = description;
        TimePeriod = new TimePeriod(startTime, endTime);
        LectureDate = date;
        ClassRoom = classRoom;
    }
    #endregion Constructors
    
    #region Lecture Methods
    
    #endregion Lecture Methods
}