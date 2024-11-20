using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.User.Application.Features.User.Query;
using Module.User.Domain.Entities;
using Module.User.Infrastructure.DbContext;
using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Dto.Features.School.User.Query;

namespace Module.User.Infrastructure.Features.User
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<GetDetailedUserResponse?>>
    {
        private readonly UserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(UserDbContext dbContext)
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
