using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Class.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Class;

public class GetClassQueryHandler : IRequestHandler<GetClassQuery, Result<GetDetailedClassResponse?>>
{
    private readonly IMapper _mapper;
    private readonly SchoolDbContext _semesterDbContext;

    public GetClassQueryHandler(SchoolDbContext semesterDbContext, IMapper mapper)
    {
        _semesterDbContext = semesterDbContext;
        _mapper = mapper;
    }

    async Task<Result<GetDetailedClassResponse?>> IRequestHandler<GetClassQuery, Result<GetDetailedClassResponse?>>.
        Handle(
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