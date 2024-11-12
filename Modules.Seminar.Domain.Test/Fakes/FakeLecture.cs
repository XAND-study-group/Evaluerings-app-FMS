using Module.Semester.Domain.Entities;
using Module.Semester.Domain.ValueObjects;

namespace Module.Semester.Domain.Test.Fakes;

public class FakeLecture : Lecture
{
    public void SetLectureTitle(string title)
    {
        LectureTitle = title;
    }

    public void SetTimePeriod(TimeOnly from, TimeOnly to)
    {
        TimePeriod = new TimePeriod(from, to);
    }

    public void SetLectureDate(DateOnly date)
    {
        LectureDate = date;
    }

    public void SetClassRoom(string value)
    {
        ClassRoom = value;
    }

    public new void AssureNoDuplicates(User teacher, List<User> teachers)
    {
        base.AssureNoDuplicates(teacher, teachers);
    }
}