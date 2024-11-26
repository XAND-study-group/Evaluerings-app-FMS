using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Infrastructure.DbContexts;

namespace Module.ExitSlip.Infrastructure.Repositories;

public class ExitSlipRepository(ExitSlipDbContext exitSlipDbContext) : IExitSlipRepository
{




    async Task IExitSlipRepository.CreateExitSlipAsync(Domain.Entities.ExitSlip exitSlip)
    {
        await exitSlipDbContext.ExitSlips.AddAsync(exitSlip);
        await exitSlipDbContext.SaveChangesAsync();
    }

    async Task IExitSlipRepository.DeleteExitSlipAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(exitSlip).Property(nameof(Domain.Entities.ExitSlip.RowVersion)).OriginalValue = rowVersion;
        exitSlipDbContext.ExitSlips.Remove(exitSlip);
        await exitSlipDbContext.SaveChangesAsync();
    }

    async Task<Domain.Entities.ExitSlip> IExitSlipRepository.GetExitSlipByIdAsync(Guid id)
    {
        return await exitSlipDbContext.ExitSlips.SingleAsync(e => e.Id == id);
    }

    async Task IExitSlipRepository.UpdateExitSlipAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(exitSlip).Property(nameof(Domain.Entities.ExitSlip.RowVersion)).OriginalValue = rowVersion;
        await exitSlipDbContext.SaveChangesAsync();
    }
}

