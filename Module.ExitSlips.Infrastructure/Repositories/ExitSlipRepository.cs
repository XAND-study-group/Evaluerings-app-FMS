using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Infrastructure.Repositories;

public class ExitSlipRepository(ExitSlipDbContext exitSlipDbContext) : IExitSlipRepository
{
    async Task IExitSlipRepository.CreateExitSlipAsync(Domain.Entities.ExitSlip exitSlip)
    {
        await exitSlipDbContext.ExitSlips.AddAsync(exitSlip);
        await exitSlipDbContext.SaveChangesAsync();
    }
}

