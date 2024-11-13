using MediatR;
using SharedKernel.Dto.Features.User.Query;

namespace Module.User.Application.Features.User.Query;

public sealed record GetUserQuery(Guid Id) : IRequest<GetUserFullResponse>;
