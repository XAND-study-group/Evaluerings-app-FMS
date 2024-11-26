using MediatR;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Answer.Query;

public sealed record GetAnswersByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<GetSimpleAnswerResponse>>>;