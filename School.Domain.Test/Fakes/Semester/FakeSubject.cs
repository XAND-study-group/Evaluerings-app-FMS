using School.Domain.Entities;
using School.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace School.Domain.Test.Fakes.Semester
{
    public class FakeSubject : Subject
    {
        #region Constructors

        public FakeSubject(SubjectName name, SubjectDescription description, IEnumerable<Subject> otherSubjects)
            : base(name, description, otherSubjects)
        {
        }

        #endregion

        #region Subject Business Logic Methods

        public static new Subject Create(SubjectName name, SubjectDescription description, IEnumerable<Subject> otherSubjects)
        {
            return Subject.Create(name, description, otherSubjects);
        }

        public void SetSubjectName(SubjectName name)
        {
            Name = name;
        }

        public void SetSubjectDescription(SubjectDescription description)
        {
            Description = description;
        }

        public new void Update(SubjectName name, SubjectDescription description)
        {
            base.Update(name, description);
        }

        public new Lecture AddLecture(string lectureTitle, string description, TimeOnly startTime,
            TimeOnly endTime, DateOnly date, string classRoom)
        {
            return base.AddLecture(lectureTitle, description, startTime, endTime, date, classRoom);
        }
        #endregion
    }
}
