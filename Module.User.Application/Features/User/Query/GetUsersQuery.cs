using MediatR;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Models;

namespace Module.User.Application.Features.User.Query;

public sealed record GetUsersQuery() : IRequest<Result<IEnumerable<GetUsersResponse?>>>;