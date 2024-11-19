using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.User.Application.Features.User.Query;
using Module.User.Infrastructure.DbContext;
using SharedKernel.Dto.Features.User.Query;
using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Infrastructure.Features.User
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<IEnumerable<GetUsersResponse?>>>
    {
        private readonly UserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(UserDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.User, GetUserResponse>();
            }).CreateMapper();
        }

        async Task<Result<IEnumerable<GetUsersResponse?>>> IRequestHandler<GetUsersQuery, Result<IEnumerable<GetUsersResponse?>>>.Handle(
            GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getUsersResponse = await _dbContext.Users
                 .AsNoTracking()
                 .ProjectTo<GetUsersResponse>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

                return Result<IEnumerable<GetUsersResponse?>>.Create("Efterspurgte Users er fundet", getUsersResponse,
                    ResultStatus.Success);

            }
            catch (Exception e)
            {
                return Result<IEnumerable<GetUsersResponse?>>.Create(e.Message, [],
                    ResultStatus.Error);
            }
        }
    }
}