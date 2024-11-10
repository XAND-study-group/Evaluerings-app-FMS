using Module.Semester.Domain.Enums;

namespace Module.Class.Domain.Test.Fakes;

public class FakeSemester : Semester.Domain.Entity.Semester
{
    public FakeSemester(string name)
    {
        Name = name;
    }
    public new void AssureEndDateAfterStartDate(DateOnly startDate, DateOnly endDate)
    {
        base.AssureEndDateAfterStartDate(startDate, endDate);
    }

    public new void AssureStartDateInFuture(DateOnly startDate, DateOnly now)
    {
        base.AssureStartDateInFuture(startDate, now);
    }
}