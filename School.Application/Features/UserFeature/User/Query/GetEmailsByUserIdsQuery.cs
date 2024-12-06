using MediatR;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.User.Query;

public record GetEmailsByUserIdsQuery(GetEmailsByUserIdsRequest Request)
    : IRequest<Result<IEnumerable<GetEmailsByUserIdsResponse>>>;