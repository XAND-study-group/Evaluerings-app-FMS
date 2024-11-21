using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.Semester;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.Semester;

public class SemesterRepository : ISemesterRepository
{
    private readonly SchoolDbContext _dbContext;

    public SemesterRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateSemesterAsync(Domain.Entities.Semester semester)
    {
        await _dbContext.AddAsync(semester);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Semester>> GetAllSemesters()
        => await _dbContext.Semesters.ToListAsync();

    public async Task AddResponsibleToSemester()
        => await _dbContext.SaveChangesAsync();

    public async Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId)
        => await _dbContext.Semesters.SingleAsync(s => s.Id == semesterId);

    public async Task<Domain.Entities.User> GetUserById(Guid userId)
        => await _dbContext.Users.SingleAsync(u => u.Id == userId);
}