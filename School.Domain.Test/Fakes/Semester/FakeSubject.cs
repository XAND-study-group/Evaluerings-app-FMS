using School.Domain.Entities;

namespace School.Domain.Test.Fakes.Semester;

public class FakeSubject : Subject
{
    #region Constructors

    public FakeSubject(string name, string description)
    {
        Name = name;
        Description = description;
    }

    #endregion

    #region Subject Business Logic Methods

    public void SetSubjectName(string name)
    {
        Name = name;
    }

    public void SetSubjectDescription(string description)
    {
        Description = description;
    }

    public new void Update(string name, string description)
    {
        base.Update(name, description);
    }

    public new Lecture AddLecture(string lectureTitle, string description, TimeOnly startTime,
        TimeOnly endTime, DateOnly date, string classRoom)
    {
        return base.AddLecture(lectureTitle, description, startTime, endTime, date, classRoom);
    }

    #endregion
}