using SharedKernel.Dto.Features.Evaluering.Answer.Query;

namespace SharedKernel.Dto.Features.Evaluering.Question.Query;

public record GetDetailedQuestionsResponse(
    Guid QuestionId,
    Guid ExitSlipId,
    string Text,
    IEnumerable<GetAnswerResponse> Answers);

