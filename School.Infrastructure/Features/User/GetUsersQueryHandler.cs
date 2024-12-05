using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.UserFeature.User.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.User
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<IEnumerable<GetSimpleUserResponse?>>>
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(SchoolDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        async Task<Result<IEnumerable<GetSimpleUserResponse?>>> IRequestHandler<GetUsersQuery, Result<IEnumerable<GetSimpleUserResponse?>>>.Handle(
            GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getUsersResponse = await _dbContext.Users
                 .AsNoTracking()
                 .ProjectTo<GetSimpleUserResponse>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

                return Result<IEnumerable<GetSimpleUserResponse?>>.Create("Efterspurgte Users er fundet", getUsersResponse,
                    ResultStatus.Success);

            }
            catch (Exception e)
            {
                return Result<IEnumerable<GetSimpleUserResponse?>>.Create(e.Message, [],
                    ResultStatus.Error);
            }
        }
    }
}