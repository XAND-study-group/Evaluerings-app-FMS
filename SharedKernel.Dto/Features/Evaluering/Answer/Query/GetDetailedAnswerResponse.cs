namespace SharedKernel.Dto.Features.Evaluering.Answer.Query;

public record GetDetailedAnswerResponse(
    Guid Id,
    string Text,
    byte[] RowVersion);