using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Class.Query;
using Module.Semester.Domain.Entities;
using Module.Semester.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Models;

namespace Module.Semester.Infrastructure.Features.Class;

public class GetClassQueryHandler : IRequestHandler<GetClassQuery, Result<GetDetailedClassResponse?>>
{
    private readonly SemesterDbContext _semesterDbContext;
    private readonly IMapper _mapper;

    public GetClassQueryHandler(SemesterDbContext semesterDbContext)
    {
        _semesterDbContext = semesterDbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Class, GetDetailedClassResponse>();
            cfg.CreateMap<User, GetClassUserResponse>();
            cfg.CreateMap<Domain.Entities.Subject, GetClassSubjectResponse>();
        }).CreateMapper();
    }

    async Task<Result<GetDetailedClassResponse?>> IRequestHandler<GetClassQuery, Result<GetDetailedClassResponse?>>.Handle(
        GetClassQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getClassResponse = await _semesterDbContext.Classes
                .AsNoTracking()
                .Where(s => s.Id == request.SeminarId)
                .ProjectTo<GetDetailedClassResponse>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return Result<GetDetailedClassResponse?>.Create("Den Specifikke klasse fundet", getClassResponse,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetDetailedClassResponse?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}