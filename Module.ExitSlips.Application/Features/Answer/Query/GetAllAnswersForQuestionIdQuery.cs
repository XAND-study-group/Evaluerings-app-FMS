using MediatR;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Answer.Query;

public sealed record GetAllAnswersForQuestionIdQuery(Guid QuestionId) : IRequest<Result<IEnumerable<GetSimpleAnswerResponse>>>;
