using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Application.Abstractions
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question question);
        Task UpdateAsync(Guid id, string newText);
        Task DeleteAsync(Guid id);
    }
}
