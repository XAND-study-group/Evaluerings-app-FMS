namespace SharedKernel.Dto.Features.Evaluering.Answer.Query;

public record GetAnswerResponse(
    Guid AnswerId,
    string Text,
    Guid UserId);