using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.User.Application.Features.User.Query;
using Module.User.Domain.Entities;
using Module.User.Infrastructure.DbContext;
using SharedKernel.Dto.Features.User.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Infrastructure.Features.User
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetSimpleUserResponse>
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

        async Task<GetSimpleUserResponse> IRequestHandler<GetUserQuery, GetSimpleUserResponse>.Handle(
            GetUserQuery request, CancellationToken cancellationToken)
        => await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == request.Id)
            .ProjectTo<GetSimpleUserResponse>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}
