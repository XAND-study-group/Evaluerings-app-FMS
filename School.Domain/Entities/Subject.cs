using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace School.Domain.Entities
{
    public class Subject : Entity
    {
        #region Properties

        public Guid Id { get; protected set; }
        public SubjectName Name { get; protected set; }
        public SubjectDescription Description { get; protected set; }
        private readonly List<Lecture> _lectures = new();
        public IReadOnlyCollection<Lecture> Lectures => _lectures;

        #endregion

        #region Constructors

        protected Subject(SubjectName name, SubjectDescription description, IEnumerable<Lecture> lectures, IEnumerable<Subject> otherSubjects)
        {
            Name = name;
            Description = description;
            _lectures = lectures.ToList();
            AssureNameIsUnique(name, otherSubjects);
        }
        
        protected Subject(SubjectName name, SubjectDescription description, IEnumerable<Subject> otherSubjects)
        {
            Name = name;
            Description = description;
            AssureNameIsUnique(name, otherSubjects);
        }

        protected Subject() { }

        #endregion

        #region Subject Business Logic Methods

        public static Subject Create(SubjectName name, SubjectDescription description, IEnumerable<Subject> otherSubjects)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (description == null) throw new ArgumentNullException(nameof(description));
            return new Subject(name, description, otherSubjects);
        }
        
        public static Subject Create(SubjectName name, SubjectDescription description, IEnumerable<Lecture> lectures, IEnumerable<Subject> otherSubjects)
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(description);
            
            return new Subject(name, description, lectures, otherSubjects);
        }

        public void Update(SubjectName name, SubjectDescription description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public Lecture AddLecture(string lectureTitle, string description, TimeOnly startTime,
            TimeOnly endTime, DateOnly date, string classRoom)
        {
            var lecture = Lecture.Create(lectureTitle, description, startTime, endTime, date, classRoom);
            _lectures.Add(lecture);
            return lecture;
        }

        public void AssureNameIsUnique(SubjectName name, IEnumerable<Subject> otherSubjects)
        {
            if (otherSubjects.Any(subject => subject.Name == name))
            {
                throw new ArgumentException($"The subject name '{name}' is already in use.");
            }
        }

        #endregion
    }
}