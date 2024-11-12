using Module.Semester.Domain.Entities;

namespace Module.Semester.Application.Abstractions;

public interface ILectureRepository
{
    Task CreateLecture(Lecture lecture);
    Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId);
}