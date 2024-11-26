using MediatR;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Question.Query;

public sealed record GetQuestionsByExitSlipIdQuery(Guid ExitSlipId) : IRequest<Result<IEnumerable<GetSimpleQuestionsResponse>>>;
