using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.Semester;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.Semester;

public class SemesterRepository(SchoolDbContext dbContext) : ISemesterRepository
{
    public async Task CreateSemesterAsync(Domain.Entities.Semester semester)
    {
        await dbContext.AddAsync(semester);
        await dbContext.SaveChangesAsync();
    }

    public async Task CreateSemestersAsync(IEnumerable<Domain.Entities.Semester> semester)
    {
        await dbContext.AddRangeAsync(semester);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Semester>> GetAllSemesters()
        => await dbContext.Semesters.ToListAsync();

    public async Task AddResponsibleToSemester()
        => await dbContext.SaveChangesAsync();

    public async Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId)
        => await dbContext.Semesters.FirstOrDefaultAsync(s => s.Id == semesterId) ??
           throw new ArgumentException("Semester ikke fundet");

    public async Task<Domain.Entities.User> GetUserById(Guid userId)
        => await dbContext.Users.SingleAsync(u => u.Id == userId);

    public async Task<bool> IsStudentInSemester(Guid semesterId, Guid userId)
        => (await dbContext.Semesters
                .Include(s => s.Classes)
                .ThenInclude(c => c.Students)
                .FirstAsync(s => s.Id == semesterId)).Classes
            .SelectMany(c => c.Students)
            .Any(s => s.Id == userId);
}