namespace SharedKernel.Dto.Features.Evaluering.Question.Command;

public record DeleteQuestionRequest(
    Guid QuestionId,
    byte[] RowVersion);
