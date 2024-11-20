using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.User.Application.Features.User.Query;
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
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<IEnumerable<GetSimpleUserResponse?>>>
    {
        private readonly UserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(UserDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.User, GetSimpleUserResponse>();
            }).CreateMapper();
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