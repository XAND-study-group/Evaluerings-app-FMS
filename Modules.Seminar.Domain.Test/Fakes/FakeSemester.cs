using Module.Semester.Domain.Entities;
using SharedKernel.ValueObjects;

namespace Module.Semester.Domain.Test.Fakes;

public class FakeSemester : Entities.Semester
{
    #region Constructors

    public FakeSemester(string name)
    {
        Name = SemesterName.Create(name, []);
    }

    public FakeSemester()
    {
    }

    #endregion Constructors

    public void SetEducationRange(DateOnly startDate, DateOnly endDate)
        => EducationRange = EducationRange.Create(startDate, endDate);

    public void SetSemesterNumber(int semesterNumber)
        => SemesterNumber = semesterNumber;

    public void SetSemesterName(string name, IEnumerable<string> otherSemesterNames)
        => Name = SemesterName.Create(name, otherSemesterNames);

    public new void AssureNoDuplicateUser(User teacher, List<User> semesterResponsibles)
    {
        base.AssureNoDuplicateUser(teacher, semesterResponsibles);
    }

    public new void AssureNoDuplicateClass(Class classToCheck, List<Class> currentClasses)
    {
        base.AssureNoDuplicateClass(classToCheck, currentClasses);
    }
}