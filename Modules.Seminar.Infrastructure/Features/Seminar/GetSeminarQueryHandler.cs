using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Seminar.Application.Features.Seminar.Query;
using Module.Seminar.Application.Features.Seminar.Query.Dto;
using Module.Seminar.Domain.Entity;
using Module.Seminar.Infrastructure.DbContexts;

namespace Module.Seminar.Infrastructure.Features.Seminar;

public class GetSeminarQueryHandler : IRequestHandler<GetSeminarQuery, GetSeminarResponse>
{
    private readonly SeminarDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSeminarQueryHandler(SeminarDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entity.Seminar, GetSeminarResponse>();
            cfg.CreateMap<User, GetSeminarUserResponse>();
            cfg.CreateMap<Subject, GetSeminarSubjectResponse>();
        }).CreateMapper();
    }
    async Task<GetSeminarResponse> IRequestHandler<GetSeminarQuery, GetSeminarResponse>.Handle(GetSeminarQuery request, CancellationToken cancellationToken)
    => await _dbContext.Seminars
        .Where(s => s.Id == request.SeminarId)
        .ProjectTo<GetSeminarResponse>(_mapper.ConfigurationProvider)
        .SingleAsync(cancellationToken);
}