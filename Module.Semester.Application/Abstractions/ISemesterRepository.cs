using Module.Semester.Domain.Entity;

namespace Module.Semester.Application.Abstractions;

public interface ISemesterRepository
{
    Task CreateSemesterAsync(Domain.Entity.Semester semester);
    Task<IEnumerable<Domain.Entity.Semester>> GetAllSemesters();
    Task AddResponsibleToSemester();
    Task<Domain.Entity.Semester> GetSemesterById(Guid semesterId);
    Task<User> GetUserById(Guid userId);
}