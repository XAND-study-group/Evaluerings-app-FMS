namespace SharedKernel.Dto.Features.Evaluering.Question.Command;

public record CreateQuestionRequest(
        Guid UserId,
        Guid ExitSlipId,
        string Text);
