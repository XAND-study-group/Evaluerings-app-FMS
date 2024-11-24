using System.Net.Mime;

namespace SharedKernel.Dto.Features.Evaluering.Question.Query;

public record GetAllAnswersResponse(Guid Id, string Text);