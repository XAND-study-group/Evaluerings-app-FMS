namespace Module.Class.Domain.Test.Fakes;

public class FakeClass : Semester.Domain.Entity.Class
{
    #region Constructors
    public FakeClass(string name)
    {
        Name = name;
    }
    #endregion
    
    #region Class Business Logic Methods
    public new void AssureCapacityIsAboveZero(int capacity)
    {
        base.AssureCapacityIsAboveZero(capacity);
    }

    public new void AssureNameIsUnique(string name, IEnumerable<FakeClass> otherClassNames)
    {
        base.AssureNameIsUnique(name, otherClassNames);
    }
    #endregion
    
    #region Relational Business Logic Methods

    public new void AssureMaxCapacityIsNotReached(int studentsCount, int studentCapacity)
    {
        base.AssureMaxCapacityIsNotReached(studentsCount, studentCapacity);
    }
    #endregion
}