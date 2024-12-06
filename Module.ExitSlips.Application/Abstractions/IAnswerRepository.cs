using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Application.Abstractions;

public interface IAnswerRepository
{
    Task<Domain.Entities.ExitSlip> GetExitSlipWithQuestionsAndAnswersByIdAsync(Guid id);
    Task CreateAnswerAsync(Answer answer);
    Task UpdateAnswerAsync(Answer answer, byte[] rowVersion);
}