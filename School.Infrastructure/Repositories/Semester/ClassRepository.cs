using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.Semester;
using School.Domain.Entities;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.Semester;

public class ClassRepository(SchoolDbContext dbContext) : IClassRepository
{
    #region Class

    async Task IClassRepository.CreateClassAsync(Class newClass)
    {
        await dbContext.Classes.AddAsync(newClass);
        await dbContext.SaveChangesAsync();
    }

    async Task<IEnumerable<Class>> IClassRepository.GetAllClassesAsync()
        => await dbContext.Classes.ToListAsync();

    async Task<Class> IClassRepository.GetClassByIdAsync(Guid classId)
        => await dbContext.Classes
            .Include(c => c.Students)
            .SingleAsync(c => c.Id == classId);

    async Task<Domain.Entities.User> IClassRepository.GetUserByIdAsync(Guid studentId)
        => await dbContext.Users.SingleAsync(u => u.Id == studentId);

    async Task IClassRepository.AddUserToClassAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    async Task<bool> IClassRepository.IsUserInClass(Guid classId, Guid userId)
        => (await dbContext.Classes
                .Include(@class => @class.Students)
                .FirstAsync(c => c.Id == classId))
            .Students.Any(s => s.Id == userId);

    public async Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId)
        => await dbContext.Semesters.SingleAsync(s => s.Id == semesterId);

    #endregion
}