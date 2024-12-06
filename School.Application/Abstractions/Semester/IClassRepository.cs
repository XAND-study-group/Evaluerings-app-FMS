using School.Domain.Entities;

namespace School.Application.Abstractions.Semester;

public interface IClassRepository
{
    Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId);
    Task<bool> IsUserInClass(Guid classId, Guid userId);
    Task<List<Class>> GetClassesByUserIdAsync(Guid userId);

    #region Class

    Task CreateClassAsync(Class newClass);
    Task<IEnumerable<Class>> GetAllClassesAsync();
    Task<Class> GetClassByIdAsync(Guid classId);
    Task<Domain.Entities.User> GetUserByIdAsync(Guid studentId);
    Task AddUserToClassAsync();

    #endregion
}