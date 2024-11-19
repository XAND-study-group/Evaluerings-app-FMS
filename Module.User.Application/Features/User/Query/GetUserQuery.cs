using MediatR;
using SharedKernel.Dto.Features.School.User.Query;

namespace Module.User.Application.Features.User.Query;

public sealed record GetUserQuery(Guid Id) : IRequest<GetUserResponse>;
