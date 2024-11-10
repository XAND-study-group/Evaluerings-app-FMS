using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Semester.Query;
using Module.Semester.Application.Features.Semester.Query.Dto;
using Module.Semester.Infrastructure.DbContexts;

namespace Module.Semester.Infrastructure.Features.Semester;

public class GetSemesterQueryHandler : IRequestHandler<GetSemesterQuery, GetSemesterResponse>
{
    private readonly SemesterDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSemesterQueryHandler(SemesterDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Domain.Entities.Semester, GetSemesterResponse>(); })
            .CreateMapper();
    }

    async Task<GetSemesterResponse> IRequestHandler<GetSemesterQuery, GetSemesterResponse>.Handle(
        GetSemesterQuery request, CancellationToken cancellationToken)
        => await _dbContext.Semesters
            .AsNoTracking()
            .Where(s => s.Id == request.SemesterId)
            .ProjectTo<GetSemesterResponse>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
}