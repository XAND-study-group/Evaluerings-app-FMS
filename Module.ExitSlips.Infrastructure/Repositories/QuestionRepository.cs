using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Infrastructure.DbContexts;

namespace Module.ExitSlip.Infrastructure.Repositories;

public class QuestionRepository(ExitSlipDbContext exitSlipDbContext) : IQuestionRepository
{
    async Task<Domain.Entities.ExitSlip> IQuestionRepository.GetExitSlipWithQuestionsAndAnswersByIdAsync(Guid id)
    {
        return await exitSlipDbContext.ExitSlips
                   .Include(e => e.Questions)
                   .ThenInclude(q => q.Answers)
                   .FirstOrDefaultAsync(e => e.Id == id) ??
               throw new ArgumentException("Kunne ikke finde ExitSlip");
    }

    async Task IQuestionRepository.CreateQuestionAsync(Question question)
    {
        await exitSlipDbContext.Questions.AddAsync(question);
        await exitSlipDbContext.SaveChangesAsync();
    }

    async Task IQuestionRepository.UpdateQuestionAsync(Question question, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(question).Property(nameof(Question.RowVersion)).OriginalValue = rowVersion;
        await exitSlipDbContext.SaveChangesAsync();
    }

    async Task IQuestionRepository.DeleteQuestionAsync(Question question, byte[] rowVersion)
    {
        exitSlipDbContext.Entry(question).Property(nameof(Question.RowVersion)).OriginalValue = rowVersion;
        await exitSlipDbContext.SaveChangesAsync();
    }
}