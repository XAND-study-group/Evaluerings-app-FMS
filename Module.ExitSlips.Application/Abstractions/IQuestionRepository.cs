using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Application.Abstractions;

public interface IQuestionRepository
{
    Task<Domain.Entities.ExitSlip> GetExitSlipWithQuestionsAndAnswersByIdAsync(Guid id);
    Task CreateQuestionAsync(Question question);
    Task CreateQuestionsAsync(IEnumerable<Question> questions);

    Task UpdateQuestionAsync(Question question, byte[] rowVersion);
    Task DeleteQuestionAsync(Question question, byte[] rowVerison);
}