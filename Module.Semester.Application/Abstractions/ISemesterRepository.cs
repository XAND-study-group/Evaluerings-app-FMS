using Module.Semester.Domain.Entities;

namespace Module.Semester.Application.Abstractions;

public interface ISemesterRepository
{
    Task CreateSemesterAsync(Domain.Entities.Semester semester);
    Task<IEnumerable<Domain.Entities.Semester>> GetAllSemesters();
    Task AddResponsibleToSemester();
    Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId);
    Task<User> GetUserById(Guid userId);
}