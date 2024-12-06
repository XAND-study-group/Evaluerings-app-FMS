namespace SharedKernel.Dto.Features.Evaluering.Question.Command;

public record DeleteQuestionRequest(
    Guid ExitSlipId,
    Guid QuestionId,
    byte[] RowVersion);
