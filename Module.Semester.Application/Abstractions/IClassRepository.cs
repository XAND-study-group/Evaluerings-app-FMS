using Module.Semester.Domain.Entities;

namespace Module.Semester.Application.Abstractions;

public interface IClassRepository
{
    #region Class
    Task CreateClassAsync(Class newClass);
    Task<IEnumerable<Class>> GetAllClassesAsync();
    Task<Class> GetClassByIdAsync(Guid classId);
    Task<User> GetUserByIdAsync(Guid studentId);
    Task AddUserToClassAsync();
    #endregion

    Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId);
}