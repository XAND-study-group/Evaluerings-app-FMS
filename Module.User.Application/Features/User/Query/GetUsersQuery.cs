using MediatR;
using SharedKernel.Dto.Features.User.Query;

namespace Module.User.Application.Features.User.Query;

public sealed record GetUsersQuery() : IRequest<IEnumerable<GetDetailedUserResponse>>;