namespace SharedKernel.Dto.Features.Evaluering.Question.Query;

public record GetSimpleQuestionsResponse(
    Guid QuestionId,
    Guid ExitSlipId,
    string Text);




//Jeg er I gang med at lave Extesions til de EXitSLips og de to andre. skal finde ud af at mappe dem ordenligt. 