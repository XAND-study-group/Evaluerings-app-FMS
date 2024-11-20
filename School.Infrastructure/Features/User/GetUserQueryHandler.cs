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
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<GetDetailedUserResponse?>>
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.User, GetSimpleUserResponse>();
            }).CreateMapper();
        }

        async Task<Result<GetDetailedUserResponse?>> IRequestHandler<GetUserQuery, Result<GetDetailedUserResponse?>>.Handle(
            GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getUserResponse = await _dbContext.Users
              .AsNoTracking()
              .Where(u => u.Id == request.Id)
              .ProjectTo<GetDetailedUserResponse>(_mapper.ConfigurationProvider)
              .SingleAsync(cancellationToken);

                return Result<GetDetailedUserResponse?>.Create("Efterspurgte User er fundet", getUserResponse
                    , ResultStatus.Success);
            }
            catch (Exception e)
            {
                return Result<GetDetailedUserResponse?>.Create(e.Message, null,
                    ResultStatus.Error);
                
            }

        }
    }
}
