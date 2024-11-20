using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.User.Application.Features.User.Query;
using Module.User.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Dto.Features.School.User.Query;

namespace Module.User.Infrastructure.Features.User
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<GetUsersResponse>>
    {
        private readonly UserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(UserDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.User, GetUsersQuery>();
            }).CreateMapper();
        }

        async Task<IEnumerable<GetUsersResponse>> IRequestHandler<GetUsersQuery, IEnumerable<GetUsersResponse>>.Handle(
            GetUsersQuery request, CancellationToken cancellationToken)
       => await _dbContext.Users
            .AsNoTracking()
            .ProjectTo<GetUsersResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}