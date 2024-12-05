using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Application.Abstractions
{
    public interface IAnswerRepository
    {
        Task AddAsync(Answer answer);
        Task UpdateAsync(Answer answer);
        Task DeleteAsync(Guid id);
    }
}
