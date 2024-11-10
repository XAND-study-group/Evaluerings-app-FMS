using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Abstractions;
using Module.Semester.Domain.Entities;
using Module.Semester.Infrastructure.DbContexts;

namespace Module.Semester.Infrastructure.Repositories;

public class SemesterRepository : ISemesterRepository
{
    private readonly SemesterDbContext _dbContext;

    public SemesterRepository(SemesterDbContext dbContext)
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
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Domain.Entities.Semester> GetSemesterById(Guid semesterId)
        => await _dbContext.Semesters.SingleAsync(s => s.Id == semesterId);

    public async Task<User> GetUserById(Guid userId)
        => await _dbContext.Users.SingleAsync(u => u.Id == userId);
}