using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Application.Abstractions;

public interface IExitSlipDbContext
{
    public DbSet<Domain.Entities.ExitSlip> ExitSlips { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}

