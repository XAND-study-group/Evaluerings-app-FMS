using Module.Semester.Domain.Entity;

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
}