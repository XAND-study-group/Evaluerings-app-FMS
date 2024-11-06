namespace Module.Seminar.Domain.Test.Fakes;

public class FakeSeminar : Entity.Seminar
{
    #region Constructors
    public FakeSeminar(string name, string description, DateTime startDate, DateTime endDate, int capacity)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Capacity = capacity;
    }

    public FakeSeminar(string name)
    {
        Name = name;
    }
    #endregion
    
    #region Seminar Business Logic Methods
    public new void AssureCapacityIsAboveZero(int capacity)
    {
        base.AssureCapacityIsAboveZero(capacity);
    }

    public new void AssureEndDateAfterStartDate(DateTime startDate, DateTime endDate)
    {
        base.AssureEndDateAfterStartDate(startDate, endDate);
    }

    public new void AssureStartDateInFuture(DateTime startDate, DateTime now)
    {
        base.AssureStartDateInFuture(startDate, now);
    }

    public new void AssureNameIsUnique(string name, IEnumerable<FakeSeminar> otherSeminarNames)
    {
        base.AssureNameIsUnique(name, otherSeminarNames);
    }
    #endregion
}