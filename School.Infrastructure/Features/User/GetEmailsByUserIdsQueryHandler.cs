using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.UserFeature.User.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.User;

public class GetEmailsByUserIdsQueryHandler(SchoolDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetEmailsByUserIdsQuery, Result<IEnumerable<GetEmailsByUserIdsResponse>>>
{
    public async Task<Result<IEnumerable<GetEmailsByUserIdsResponse>>> Handle(GetEmailsByUserIdsQuery request,
        CancellationToken cancellationToken)
    {
        var responseData = await dbContext.Users.Where(user => request.Request.UserIds.Contains(user.Id))
            .Select(user => user.Email)
            .ProjectTo<GetEmailsByUserIdsResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result<IEnumerable<GetEmailsByUserIdsResponse>>.Create(
            "Alle emails på brugere er skaffet",
            responseData,
            ResultStatus.Success);
    }
}