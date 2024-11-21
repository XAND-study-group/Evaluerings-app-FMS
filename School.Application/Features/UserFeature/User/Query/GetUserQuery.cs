using MediatR;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.User.Query;

public sealed record GetUserQuery(Guid Id) : IRequest<Result<GetDetailedUserResponse?>>;
