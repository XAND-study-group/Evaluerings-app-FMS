namespace SharedKernel.Dto.Features.Evaluering.Question.Command;

public record CreateQuestionRequest(
    Guid ExitSlipId,
    string Text);