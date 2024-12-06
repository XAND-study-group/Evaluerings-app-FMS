using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Semester.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Semester;

public class GetSemesterQueryHandler : IRequestHandler<GetSemesterQuery, Result<GetDetailedSemesterResponse?>>
{
    private readonly SchoolDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSemesterQueryHandler(SchoolDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    async Task<Result<GetDetailedSemesterResponse?>>
        IRequestHandler<GetSemesterQuery, Result<GetDetailedSemesterResponse?>>.Handle(
            GetSemesterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getSemesterResponse = await _dbContext.Semesters
                .AsNoTracking()
                .Where(s => s.Id == request.SemesterId)
                .ProjectTo<GetDetailedSemesterResponse>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return Result<GetDetailedSemesterResponse?>.Create("Efterspurgte Semester fundet", getSemesterResponse,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetDetailedSemesterResponse?>.Create(e.Message, null,
                ResultStatus.Error);
        }
    }
}