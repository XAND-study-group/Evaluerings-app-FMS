using MediatR;
using SharedKernel.Dto.Features.School.User.Query;

namespace Module.User.Application.Features.User.Query;

public sealed record GetUsersQuery() : IRequest<IEnumerable<GetUsersResponse>>;