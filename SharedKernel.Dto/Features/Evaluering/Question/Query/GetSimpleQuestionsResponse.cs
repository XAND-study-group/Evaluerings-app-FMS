using SharedKernel.Dto.Features.Evaluering.Answer.Query;

namespace SharedKernel.Dto.Features.Evaluering.Question.Query;

public record GetSimpleQuestionsResponse(
    Guid QuestionId,
    Guid ExitSlipId,
    string Text,
    IEnumerable<GetSimpleAnswerResponse> Answers,
    byte[] RowVersion);
