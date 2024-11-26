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

public class ExitSlipRepository(ExitSlipDbContext _context) : IExitSlipRepository
{

    public async Task CreateExitSlipAsync(Domain.Entities.ExitSlip exitSlip)
    {
        await _context.ExitSlips.AddAsync(exitSlip);
        await _context.SaveChangesAsync();
    }
    public async Task<Domain.Entities.ExitSlip> GetExitSlipByIdAsync(Guid exitSlipId)
    {
        return await _context.ExitSlips
                .Include(e => e.Questions)
                .ThenInclude(q => q.Answers)
                .SingleAsync(i => i.Id == exitSlipId);
    }

    public async Task UpdateAnswerAsync(Answer answer, byte[] rowVersion)
    {
        _context.Entry(answer).Property(nameof(Answer.RowVersion)).OriginalValue = rowVersion;
        _context.Answers.Update(answer);
        await _context.SaveChangesAsync();
    }
}

