namespace Module.Seminar.Domain.Test.Fakes;

public class FakeSeminar : Entity.Seminar
{
    #region Constructors
    public FakeSeminar(string name, string description, DateOnly startDate, DateOnly endDate, int studentCapacity)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        StudentCapacity = studentCapacity;
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

    public new void AssureEndDateAfterStartDate(DateOnly startDate, DateOnly endDate)
    {
        base.AssureEndDateAfterStartDate(startDate, endDate);
    }

    public new void AssureStartDateInFuture(DateOnly startDate, DateOnly now)
    {
        base.AssureStartDateInFuture(startDate, now);
    }

    public new void AssureNameIsUnique(string name, IEnumerable<FakeSeminar> otherSeminarNames)
    {
        base.AssureNameIsUnique(name, otherSeminarNames);
    }
    #endregion
    
    #region Relational Business Logic Methods

    public new void AssureMaxCapacityIsNotReached(int studentsCount, int capacity)
    {
        base.AssureMaxCapacityIsNotReached(studentsCount, capacity);
    }
    #endregion
}