using MediatR;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Question.Query;

public sealed record GetAllQuestionsQuery() : IRequest<Result<IEnumerable<GetSimpleQuestionsResponse>>>;
