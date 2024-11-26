namespace SharedKernel.Dto.Features.Evaluering.Answer.Command;

public record CreateAnswerRequest(
        Guid QuestionId,
        Guid ExitslipId,
        string Text);
