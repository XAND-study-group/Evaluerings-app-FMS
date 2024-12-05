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
        => await _semesterDbContext.Classes.FirstOrDefaultAsync(c => c.Id == classId) ??
           throw new ArgumentException("Klasse ikke fundet");

    async Task<Domain.Entities.User> IClassRepository.GetUserByIdAsync(Guid studentId)
        => await _semesterDbContext.Users.FirstOrDefaultAsync(u => u.Id == studentId) ??
           throw new ArgumentException("Bruger ikke fundet");

    async Task IClassRepository.AddUserToClassAsync()
    {
        await _semesterDbContext.SaveChangesAsync();
    }

    public async Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId)
        => await _semesterDbContext.Semesters.FirstOrDefaultAsync(s => s.Id == semesterId) ??
           throw new ArgumentException("Semester ikke fundet");

    #endregion
}