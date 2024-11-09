using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Abstractions;
using Module.Semester.Infrastructure.DbContexts;
using Module.Semester.Domain.Entity;

namespace Module.Semester.Infrastructure.Repositories;

public class ClassRepository : IClassRepository
{
    private readonly SemesterDbContext _semesterDbContext;

    public ClassRepository(SemesterDbContext semesterDbContext)
    {
        _semesterDbContext = semesterDbContext;
    }

    #region Class

    async Task IClassRepository.CreateClassAsync(Module.Semester.Domain.Entity.Class newClass)
    {
        await _semesterDbContext.Classes.AddAsync(newClass);
        await _semesterDbContext.SaveChangesAsync();
    }

    async Task<IEnumerable<Class>> IClassRepository.GetAllClassesAsync()
        => await _semesterDbContext.Classes.ToListAsync();

    async Task<Class> IClassRepository.GetClassByIdAsync(Guid classId)
        => await _semesterDbContext.Classes.SingleAsync(s => s.Id == classId);

    async Task<User> IClassRepository.GetUserByIdAsync(Guid studentId)
        => await _semesterDbContext.Users.SingleAsync(s => s.Id == studentId);

    async Task IClassRepository.AddUserToClassAsync()
    {
        await _semesterDbContext.SaveChangesAsync();
    }

    #endregion
}