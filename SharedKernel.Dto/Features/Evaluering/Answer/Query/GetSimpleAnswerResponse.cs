namespace SharedKernel.Dto.Features.Evaluering.Answer.Query;

public record GetSimpleAnswerResponse(
    Guid AnswerId,
    string Text,
    Guid QuestionId,
    byte[] RowVersion);