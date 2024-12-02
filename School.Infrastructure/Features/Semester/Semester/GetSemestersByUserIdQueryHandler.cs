using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Semester.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Semester;

public class GetSemestersByUserIdQueryHandler : IRequestHandler<GetSemestersByUserIdQuery, Result<IEnumerable<GetSimpleSemesterResponse>>>
{
    private readonly SchoolDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSemestersByUserIdQueryHandler(SchoolDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    async Task<Result<IEnumerable<GetSimpleSemesterResponse>>> IRequestHandler<GetSemestersByUserIdQuery, Result<IEnumerable<GetSimpleSemesterResponse>>>.Handle(GetSemestersByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getSemesterResponses = await _dbContext.Semesters
                .AsNoTracking()
                .Where(s => s.SemesterResponsibles.Any(u => u.Id == request.UserId))
                .ProjectTo<GetSimpleSemesterResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetSimpleSemesterResponse>>.Create("Fandt semestre tilknyttet bruger",
                getSemesterResponses, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetSimpleSemesterResponse>>.Create(e.Message,
                [], ResultStatus.Error);
        }
    }
}