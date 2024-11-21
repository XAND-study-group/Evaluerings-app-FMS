using School.Domain.Entities;
using School.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace School.Domain.Test.Fakes.Semester;

public class FakeLecture : Lecture
{
    public void SetLectureTitle(string title)
    {
        Title = title;
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

    public new void AssureNoDuplicates(Entities.User teacher, List<Entities.User> teachers)
    {
        base.AssureNoDuplicates(teacher, teachers);
    }
}