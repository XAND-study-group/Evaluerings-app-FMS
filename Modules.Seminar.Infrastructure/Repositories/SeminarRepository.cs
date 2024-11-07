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
    async Task ISeminarRepository.CreateSeminarAsync(Module.Seminar.Domain.Entity.Seminar seminar)
    {
        await _seminarDbContext.Seminars.AddAsync(seminar);
        await _seminarDbContext.SaveChangesAsync();
    }

    async Task<IEnumerable<Module.Seminar.Domain.Entity.Seminar>> ISeminarRepository.GetAllSeminars()
        => await _seminarDbContext.Seminars.ToListAsync();
}