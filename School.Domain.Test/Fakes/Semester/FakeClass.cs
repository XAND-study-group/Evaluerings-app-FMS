using School.Domain.Entities;

namespace School.Domain.Test.Fakes.Semester;

public class FakeClass : Class
{
    #region Relational Business Logic Methods

    public new void AssureMaxCapacityIsNotReached(int studentsCount, int studentCapacity)
    {
        base.AssureMaxCapacityIsNotReached(studentsCount, studentCapacity);
    }

    #endregion

    #region Constructors

    public FakeClass(string name)
    {
        Name = name;
    }

    public FakeClass()
    {
    }

    public FakeClass(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    #endregion

    #region Class Business Logic Methods

    public new void AssureNameIsUnique(string name, IEnumerable<FakeClass> otherClassNames)
    {
        base.AssureNameIsUnique(name, otherClassNames);
    }

    public void SetStudentCapacity(int capacity)
    {
        StudentCapacity = capacity;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }

    #endregion
}