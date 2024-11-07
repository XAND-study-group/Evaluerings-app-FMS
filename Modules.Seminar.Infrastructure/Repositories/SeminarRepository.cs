using Microsoft.EntityFrameworkCore;
using Module.Seminar.Application.Abstractions;
using Module.Seminar.Infrastructure.DbContexts;

namespace Modules.Seminar.Infrastructure.Repositories;

public class SeminarRepository : ISeminarRepository
{
    private readonly SeminarDbContext _seminarDbContext;

    public SeminarRepository(SeminarDbContext seminarDbContext)
    {
        _seminarDbContext = seminarDbContext;
    }

    #region Seminar

    async Task ISeminarRepository.CreateSeminarAsync(Module.Seminar.Domain.Entity.Seminar seminar)
    {
        await _seminarDbContext.Seminars.AddAsync(seminar);
        await _seminarDbContext.SaveChangesAsync();
    }

    async Task<IEnumerable<Module.Seminar.Domain.Entity.Seminar>> ISeminarRepository.GetAllSeminarsAsync()
        => await _seminarDbContext.Seminars.ToListAsync();

    async Task<Module.Seminar.Domain.Entity.Seminar> ISeminarRepository.GetSeminarByIdAsync(Guid seminarId)
        => await _seminarDbContext.Seminars.SingleAsync(s => s.Id == seminarId);

    async Task<User> ISeminarRepository.GetStudentByIdAsync(Guid studentId)
        => await _seminarDbContext.User.SingleAsync(s => s.Id == studentId);

    async Task ISeminarRepository.AddStudentToSeminarAsync(Module.Seminar.Domain.Entity.Seminar seminar)
    {
        await _seminarDbContext.SaveChangesAsync();
    }

    #endregion
}