using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Application.Abstractions;

public interface IAnswerRepository
{
    Task<Domain.Entities.ExitSlip> GetExitSlipWithQuestionsAndAnswersByIdAsync(Guid id);
    Task CreateAnswerAsync(Answer answer);
    Task CreateAnswersAsync(IEnumerable<Answer> answers);

    Task UpdateAnswerAsync(Answer answer, byte[] rowVersion);
}