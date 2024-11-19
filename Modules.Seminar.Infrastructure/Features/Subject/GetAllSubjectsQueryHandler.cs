using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Subject.Query;
using Module.Semester.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Dto.Features.Subject.Query;
using SharedKernel.Models;

namespace Module.Semester.Infrastructure.Features.Subject
{
    public class GetAllSubjectsQueryHandler(SemesterDbContext _semesterDbContext) : IRequestHandler<GetAllSubjectsQuery, Result<IEnumerable<GetDetailedSubjectResponse>>>
    {
        
        async Task<Result<IEnumerable<GetDetailedSubjectResponse>>> 
        IRequestHandler<GetAllSubjectsQuery,Result<IEnumerable<GetDetailedSubjectResponse>>>.Handle(
            GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjects = await _semesterDbContext.Subjects
                    .AsNoTracking()
                    .ProjectTo<GetDetailedSubjectResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<GetDetailedSubjectResponse>>.Create("Alle fag er fundet", subjects,
                    ResultStatus.Success);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<GetDetailedSubjectResponse>>.Create(ex.Message, [], ResultStatus.Error);

            }
        }
    }
}
