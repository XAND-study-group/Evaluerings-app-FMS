using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.Semester;
using School.Domain.Entities;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.Semester;

public class ClassRepository : IClassRepository
{
    private readonly SchoolDbContext _semesterDbContext;

    public ClassRepository(SchoolDbContext semesterDbContext)
    {
        _semesterDbContext = semesterDbContext;
    }

    #region Class

    async Task IClassRepository.CreateClassAsync(Class newClass)
    {
        await _semesterDbContext.Classes.AddAsync(newClass);
        await _semesterDbContext.SaveChangesAsync();
    }

    async Task<IEnumerable<Class>> IClassRepository.GetAllClassesAsync()
        => await _semesterDbContext.Classes.ToListAsync();

    async Task<Class> IClassRepository.GetClassByIdAsync(Guid classId)
        => await _semesterDbContext.Classes
            .Include(c => c.Students)
            .SingleAsync(c => c.Id == classId);

    async Task<Domain.Entities.User> IClassRepository.GetUserByIdAsync(Guid studentId)
        => await _semesterDbContext.Users.SingleAsync(u => u.Id == studentId);

    async Task IClassRepository.AddUserToClassAsync()
    {
        await _semesterDbContext.SaveChangesAsync();
    }

    public async Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId)
        => await _semesterDbContext.Semesters.SingleAsync(s => s.Id == semesterId);

    #endregion
}