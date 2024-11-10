namespace Module.Semester.Domain.Test.Fakes;

public class FakeSemester : Entities.Semester
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