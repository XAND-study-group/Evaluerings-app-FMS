namespace SharedKernel.Dto.Features.Evaluering.Question.Command;

public record UpdateQuestionRequest(
    Guid QuestionId,
    Guid ExitSlipId,
    string Text,
    byte[] RowVersion);