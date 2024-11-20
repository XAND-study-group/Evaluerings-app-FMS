using School.Domain.Entities;

namespace School.Application.Abstractions.Semester;

public interface ILectureRepository
{
    Task CreateLecture(Lecture lecture);
    Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId);
}