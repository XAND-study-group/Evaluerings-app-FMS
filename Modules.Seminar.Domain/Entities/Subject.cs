namespace Module.Semester.Domain.Entities
{
    public class Subject : Entity
    {
        private readonly List<Lecture> _lectures = [];
        public IReadOnlyCollection<Lecture> Lectures => _lectures;

        public static Subject Create()
        {
            return new Subject();
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