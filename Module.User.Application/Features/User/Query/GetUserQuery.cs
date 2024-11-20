using MediatR;
using SharedKernel.Dto.Features.User.Query;
using SharedKernel.Models;

namespace Module.User.Application.Features.User.Query;

public sealed record GetUserQuery(Guid Id) : IRequest<Result<GetUserResponse?>>;
