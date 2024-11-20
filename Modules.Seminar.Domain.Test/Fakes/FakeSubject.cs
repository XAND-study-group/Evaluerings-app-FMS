using Module.Semester.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.ValueObjects;

namespace Module.Semester.Domain.Test.Fakes
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
