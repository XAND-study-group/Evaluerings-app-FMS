namespace SharedKernel.Dto.Features.Evaluering.Answer.Command;

public record UpdateAnswerRequest(
    Guid QuestionId,
    Guid AnswerId,
    Guid ExitSlipId,
    string Text,
    byte[] RowVersion);