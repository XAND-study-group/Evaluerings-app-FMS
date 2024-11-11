using System.Collections;
using Module.Semester.Domain.Entities;
using Module.Semester.Domain.ValueObjects;

namespace Module.Semester.Domain.Test.Fakes;

public class FakeSemester : Entities.Semester
{
    public FakeSemester(string name)
    {
        Name = name;
    }

    public new void AssureNameIsUnique(string name, IEnumerable<Entities.Semester> otherSemesters)
        => base.AssureNameIsUnique(name, otherSemesters);
    
    public void SetEducationRange(DateOnly startDate, DateOnly endDate)
        => EducationRange = EducationRange.Create(startDate, endDate);

    public void SetSemesterNumber(int semesterNumber)
        => SemesterNumber = semesterNumber;

    public new void AssureNoDuplicateUser(User teacher, List<User> semesterResponsibles)
    {
        base.AssureNoDuplicateUser(teacher, semesterResponsibles);
    }
}