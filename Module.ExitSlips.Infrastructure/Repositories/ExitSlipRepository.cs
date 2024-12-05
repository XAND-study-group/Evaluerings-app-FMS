﻿using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Module.ExitSlip.Infrastructure.Repositories;

public class ExitSlipRepository(ExitSlipDbContext exitSlipDbContext) : IExitSlipRepository
{
    public async Task<Domain.Entities.ExitSlip> GetExitSlipByIdAsync(Guid exitSlipId)
    {
        return await exitSlipDbContext.ExitSlips
                .FirstOrDefaultAsync(e => e.Id == exitSlipId) ??
                throw new ArgumentException("Kunne ikke finde ExitSlip");
    }
    async Task<Domain.Entities.ExitSlip> IExitSlipRepository.GetExitSlipWithQuestionsAndAnswersByIdAsync(Guid id)
    {
        return await exitSlipDbContext.ExitSlips
             .Include(e => e.Questions)
             .ThenInclude(q => q.Answers)
             .FirstOrDefaultAsync(e => e.Id == id) ??
             throw new ArgumentException("Kunne ikke finde ExitSlip");
    }

    async Task IExitSlipRepository.CreateExitSlipAsync(Domain.Entities.ExitSlip exitSlip)
    {
        await exitSlipDbContext.ExitSlips.AddAsync(exitSlip);
        await exitSlipDbContext.SaveChangesAsync();
    }
    async Task IExitSlipRepository.UpdateExitSlipAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(exitSlip).Property(nameof(Domain.Entities.ExitSlip.RowVersion)).OriginalValue = rowVersion;
        await exitSlipDbContext.SaveChangesAsync();
    }
    async Task IExitSlipRepository.UpdateExitSlipActiveStatusAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(exitSlip).Property(nameof(Domain.Entities.ExitSlip.RowVersion)).OriginalValue = rowVersion;
        await exitSlipDbContext.SaveChangesAsync();
    }

    async Task IExitSlipRepository.DeleteExitSlipAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(exitSlip).Property(nameof(Domain.Entities.ExitSlip.RowVersion)).OriginalValue = rowVersion;
        exitSlipDbContext.ExitSlips.Remove(exitSlip);
        await exitSlipDbContext.SaveChangesAsync();
    }
   
}

