using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Infrastructure.DbContexts;

namespace Module.ExitSlip.Infrastructure.Repositories;

public class ExitSlipRepository(ExitSlipDbContext exitSlipDbContext) : IExitSlipRepository
{

    public async Task CreateExitSlipAsync(Domain.Entities.ExitSlip exitSlip)
    {
        await exitSlipDbContext.ExitSlips.AddAsync(exitSlip);
        await exitSlipDbContext.SaveChangesAsync();
    }
    public async Task<Domain.Entities.ExitSlip> GetExitSlipByIdAsync(Guid exitSlipId)
    {
        return await exitSlipDbContext.ExitSlips
                .Include(e => e.Questions)
                .ThenInclude(q => q.Answers)
                .SingleAsync(i => i.Id == exitSlipId);
    }
    public async Task<Question> GetQuestionByIdAsync(Guid questionId)
    {
        return await exitSlipDbContext.Questions
                .Include(q => q.Answers)
                .SingleAsync(i => i.Id == questionId);
    }

    public async Task<Domain.Entities.ExitSlip> GetExitSlipByQuestionIdAsync(Guid questionId)
    {
        return await exitSlipDbContext.ExitSlips
            .Include(e => e.Questions)
            .ThenInclude(q => q.Answers)
            .SingleAsync(e => e.Questions.Any(q => q.Id == questionId));
    }

    public async Task UpdateAnswerAsync(Answer answer, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(answer).Property(nameof(Answer.RowVersion)).OriginalValue = rowVersion;
        exitSlipDbContext.Answers.Update(answer);
        await exitSlipDbContext.SaveChangesAsync();
    }

    public async Task CreateAnswerAsync(Answer answer)
    {
        await exitSlipDbContext.Answers.AddAsync(answer);
        await exitSlipDbContext.SaveChangesAsync();
    }

    public async Task UpdateQuestionAsync(Question question, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(question).Property(nameof(Question.RowVersion)).OriginalValue = rowVersion;
        exitSlipDbContext.Questions.Update(question);
        await exitSlipDbContext.SaveChangesAsync();
    }

    public async Task CreateQuestionAsync(Question question)
    {
        await exitSlipDbContext.Questions.AddAsync(question);
        await exitSlipDbContext.SaveChangesAsync();
    }

    public async Task DeleteQuestionAsync(Question question, byte[] RowVersion)
    {
        if (question is null)
            throw new InvalidOperationException("Spørgsmål ikke fundet.");

        exitSlipDbContext.Entry(question).Property(nameof(Question.RowVersion)).OriginalValue = RowVersion;
        exitSlipDbContext.Questions.Remove(question);
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

    async Task IExitSlipRepository.UpdateExitSlipActiveStatusAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(exitSlip).Property(nameof(Domain.Entities.ExitSlip.RowVersion)).OriginalValue = rowVersion;
        await exitSlipDbContext.SaveChangesAsync();
    }

    async Task IExitSlipRepository.UpdateExitSlipAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(exitSlip).Property(nameof(Domain.Entities.ExitSlip.RowVersion)).OriginalValue = rowVersion;
        await exitSlipDbContext.SaveChangesAsync();
    }
}

