using Module.ExitSlip.Application.Abstractions;
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
    public async Task<Domain.Entities.Question> GetQuestionByIdAsync(Guid questionId)
    {
        return await _context.Questions
                .Include(q => q.Answers)
                .SingleAsync(i => i.Id == questionId);
    }

    public async Task<Domain.Entities.ExitSlip> GetExitSlipByQuestionIdAsync(Guid questionId)
    {
        return await _context.ExitSlips
            .Include(e => e.Questions)
            .ThenInclude(q => q.Answers)
            .SingleOrDefaultAsync(e => e.Questions.Any(q => q.Id == questionId));
    }

    public async Task UpdateAnswerAsync(Answer answer, byte[] rowVersion)
    {
        _context.Entry(answer).Property(nameof(Answer.RowVersion)).OriginalValue = rowVersion;
        _context.Answers.Update(answer);
        await _context.SaveChangesAsync();
    }

    public async Task CreateAnswerAsync(Answer answer)
    {
        await _context.Answers.AddAsync(answer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQuestionAsync(Question question, byte[] rowVersion)
    {
        _context.Entry(question).Property(nameof(Question.RowVersion)).OriginalValue = rowVersion;
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
    }

    public async Task CreateQuestionAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteQuestionAsync(Question question, byte[] RowVersion)
    {
        if (question is null)
            throw new InvalidOperationException("Spørgsmål ikke fundet.");

        _context.Entry(question).Property(nameof(Question.RowVersion)).OriginalValue = RowVersion;
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
    }
}

