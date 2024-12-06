using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Lecture.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Lecture;

public class GetLectureQueryHandler : IRequestHandler<GetLectureQuery, Result<GetDetailedLectureResponse?>>
{
    private readonly SchoolDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetLectureQueryHandler(SchoolDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    async Task<Result<GetDetailedLectureResponse?>>
        IRequestHandler<GetLectureQuery, Result<GetDetailedLectureResponse?>>.Handle(GetLectureQuery request,
            CancellationToken cancellationToken)
    {
        try
        {
            var getLectureResponse = await _dbContext.Lectures
                .AsNoTracking()
                .Where(l => l.Id == request.lectureId)
                .ProjectTo<GetDetailedLectureResponse>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return Result<GetDetailedLectureResponse?>.Create("Specifikke Lektion fundet", getLectureResponse,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetDetailedLectureResponse?>.Create(e.Message, null,
                ResultStatus.Error);
        }
    }
}