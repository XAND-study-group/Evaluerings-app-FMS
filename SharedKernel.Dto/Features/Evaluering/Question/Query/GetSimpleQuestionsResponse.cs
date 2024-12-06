namespace SharedKernel.Dto.Features.Evaluering.Question.Query;

public record GetSimpleQuestionsResponse(
    Guid Id,
    string Text,
    byte[] RowVersion);