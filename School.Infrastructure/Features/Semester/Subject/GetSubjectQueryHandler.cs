using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Subject.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Subject;

public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, Result<GetDetailedSubjectResponse?>>
{
    private readonly IMapper _mapper;
    private readonly SchoolDbContext _semesterDbContext;

    public GetSubjectQueryHandler(SchoolDbContext semesterDbContext, IMapper mapper)
    {
        _semesterDbContext = semesterDbContext;
        _mapper = mapper;
    }

    async Task<Result<GetDetailedSubjectResponse?>>
        IRequestHandler<GetSubjectQuery, Result<GetDetailedSubjectResponse?>>.Handle(
            GetSubjectQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getSubjectResponse = await _semesterDbContext.Subjects
                .AsNoTracking()
                .Where(s => s.Id == request.GetSubjectRequest.Id)
                .ProjectTo<GetDetailedSubjectResponse>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return Result<GetDetailedSubjectResponse?>.Create("Det Specifikke fag fundet", getSubjectResponse,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetDetailedSubjectResponse?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}