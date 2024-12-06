using SharedKernel.Dto.Features.Evaluering.Answer.Query;

namespace SharedKernel.Dto.Features.Evaluering.Question.Query;

public record GetDetailedQuestionsResponse(
    Guid Id,
    string Text,
    IEnumerable<GetDetailedAnswerResponse> Answers,
    byte[] RowVersion);