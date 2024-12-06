using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Lecture.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Lecture;

public class
    GetLecturesByUserIdQueryHandler : IRequestHandler<GetLecturesByUserIdQuery,
    Result<IEnumerable<GetSimpleLectureResponse>>>
{
    private readonly SchoolDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetLecturesByUserIdQueryHandler(SchoolDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    async Task<Result<IEnumerable<GetSimpleLectureResponse>>>
        IRequestHandler<GetLecturesByUserIdQuery, Result<IEnumerable<GetSimpleLectureResponse>>>.Handle(
            GetLecturesByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getAllLecturesResponse = await _dbContext.Classes
                .AsNoTracking()
                .Include(c => c.Students)
                .Include(c => c.Subjects)
                .ThenInclude(s => s.Lectures)
                .Where(c => c.Students
                    .Any(u => u.Id == request.userId))
                .Select(c => c.Subjects
                    .Select(s => s.Lectures))
                .AsSplitQuery()
                .ProjectTo<GetSimpleLectureResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetSimpleLectureResponse>>.Create("Lektioner tilknyttet bruger fundet",
                getAllLecturesResponse, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetSimpleLectureResponse>>.Create(e.Message, [],
                ResultStatus.Error);
        }
    }
}