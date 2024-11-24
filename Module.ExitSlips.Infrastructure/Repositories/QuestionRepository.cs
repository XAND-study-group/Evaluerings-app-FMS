using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Infrastructure.DbContexts;

namespace Module.ExitSlip.Infrastructure.Repositories
{
    public class QuestionRepository(ExitSlipDbContext exitSlipDbContext) : IQuestionRepository
    {
        private readonly ExitSlipDbContext _context = exitSlipDbContext;

        public async Task<Question> GetByIdAsync(Guid id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await Task.FromResult(_context.Questions.ToList());
        }

        public async Task AddAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, string newText)
        {
            var existingQuestion = await _context.Questions.FindAsync(id);
            if (existingQuestion != null)
            {
                existingQuestion.UpdateQuestion(newText);
                _context.Questions.Update(existingQuestion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }
    }
}
