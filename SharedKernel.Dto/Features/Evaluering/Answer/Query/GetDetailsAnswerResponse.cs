namespace SharedKernel.Dto.Features.Evaluering.Answer.Query;

public record GetDetailsAnswerResponse(
    Guid AnswerId,
    string Text,
    Guid QuestionId,
    byte[] RowVersion);