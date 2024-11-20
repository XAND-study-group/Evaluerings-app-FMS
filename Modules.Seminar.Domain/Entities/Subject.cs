using SharedKernel.Models;

namespace Module.Semester.Domain.Entities
{
    public class Subject : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        private readonly List<Lecture> _lectures = [];
        public IReadOnlyCollection<Lecture> Lectures => _lectures;

        protected Subject(string name, string description)
        {
            Name = name;
            Description = description;
        }
        protected Subject() { }

        public static Subject Create(string name, string description)
        {
            return new Subject(name, description);
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public Lecture AddLecture(string lectureTitle, string description, TimeOnly startTime,
            TimeOnly endTime, DateOnly date, string classRoom)
        {
            var lecture = Lecture.Create(lectureTitle, description, startTime, endTime, date, classRoom);
            _lectures.Add(lecture);
            return lecture;
        }
    }
}