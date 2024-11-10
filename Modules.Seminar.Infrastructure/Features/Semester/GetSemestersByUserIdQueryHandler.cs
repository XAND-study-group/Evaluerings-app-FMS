using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Semester.Query;
using Module.Semester.Application.Features.Semester.Query.Dto;
using Module.Semester.Domain.Entity;
using Module.Semester.Infrastructure.DbContexts;

namespace Module.Semester.Infrastructure.Features.Semester;

public class GetSemestersByUserIdQueryHandler : IRequestHandler<GetSemestersByUserIdQuery, IEnumerable<GetSemesterResponse>>
{
    private readonly SemesterDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSemestersByUserIdQueryHandler(SemesterDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entity.Semester, GetSemestersResponse>();
        }).CreateMapper();
    }

    async Task<IEnumerable<GetSemesterResponse>> IRequestHandler<GetSemestersByUserIdQuery, IEnumerable<GetSemesterResponse>>.Handle(GetSemestersByUserIdQuery request, CancellationToken cancellationToken)
    => await _dbContext.Semesters
        .AsNoTracking()
        .Where(s => s.SemesterResponsibles.Any(u => u.Id == request.UserId))
        .ProjectTo<GetSemesterResponse>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);
}