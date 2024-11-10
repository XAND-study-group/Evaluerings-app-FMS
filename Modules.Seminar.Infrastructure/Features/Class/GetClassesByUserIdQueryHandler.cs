using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Class.Query;
using Module.Semester.Application.Features.Class.Query.Dto;
using Module.Semester.Infrastructure.DbContexts;

namespace Module.Semester.Infrastructure.Features.Class;

public class GetClassesByUserIdQueryHandler : IRequestHandler<GetClassesByUserIdQuery, IEnumerable<GetClassesResponse>>
{
    private readonly SemesterDbContext _semesterDbContext;
    private readonly IMapper _mapper;

    public GetClassesByUserIdQueryHandler(SemesterDbContext semesterDbContext)
    {
        _semesterDbContext = semesterDbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Class, GetClassesResponse>();
        }).CreateMapper();
    }


    async Task<IEnumerable<GetClassesResponse>> IRequestHandler<GetClassesByUserIdQuery,
        IEnumerable<GetClassesResponse>>.Handle(GetClassesByUserIdQuery request,
        CancellationToken cancellationToken)
        => await _semesterDbContext.Classes
            .AsNoTracking()
            .Where(s => s.Students.Any(st => st.Id == request.UserId))
            .ProjectTo<GetClassesResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}