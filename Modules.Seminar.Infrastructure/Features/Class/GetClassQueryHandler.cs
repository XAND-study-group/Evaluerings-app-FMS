using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Class.Query;
using Module.Semester.Application.Features.Class.Query.Dto;
using Module.Semester.Domain.Entities;
using Module.Semester.Infrastructure.DbContexts;

namespace Module.Semester.Infrastructure.Features.Class;

public class GetClassQueryHandler : IRequestHandler<GetClassQuery, GetClassResponse>
{
    private readonly SemesterDbContext _semesterDbContext;
    private readonly IMapper _mapper;

    public GetClassQueryHandler(SemesterDbContext semesterDbContext)
    {
        _semesterDbContext = semesterDbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Class, GetClassResponse>();
            cfg.CreateMap<User, GetClassUserResponse>();
            cfg.CreateMap<Subject, GetClassSubjectResponse>();
        }).CreateMapper();
    }
    async Task<GetClassResponse> IRequestHandler<GetClassQuery, GetClassResponse>.Handle(GetClassQuery request, CancellationToken cancellationToken)
    => await _semesterDbContext.Classes
        .AsNoTracking()
        .Where(s => s.Id == request.SeminarId)
        .ProjectTo<GetClassResponse>(_mapper.ConfigurationProvider)
        .SingleAsync(cancellationToken);
}