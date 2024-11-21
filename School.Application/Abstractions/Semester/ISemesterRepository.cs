namespace School.Application.Abstractions.Semester;

public interface ISemesterRepository
{
    Task CreateSemesterAsync(Domain.Entities.Semester semester);
    Task<IEnumerable<Domain.Entities.Semester>> GetAllSemesters();
    Task AddResponsibleToSemester();
    Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId);
    Task<Domain.Entities.User> GetUserById(Guid userId);
}