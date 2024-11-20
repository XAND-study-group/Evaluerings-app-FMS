using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Subject.Query;
using School.Domain.Extension;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Subject
{
    public class GetAllSubjectsQueryHandler(SchoolDbContext _semesterDbContext) : IRequestHandler<GetAllSubjectsQuery, Result<IEnumerable<GetSimpleSubjectResponse>>>
    {
        
        async Task<Result<IEnumerable<GetSimpleSubjectResponse>>> 
        IRequestHandler<GetAllSubjectsQuery,Result<IEnumerable<GetSimpleSubjectResponse>>>.Handle(
            GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjects = await _semesterDbContext.Subjects
                    .Include(s => s.Lectures)
                    .ToListAsync(cancellationToken);

                var subjectResponses = subjects.Select(s => s.MapToGetSimpleSubjectResponse());

                return Result<IEnumerable<GetSimpleSubjectResponse>>.Create("Alle fag er fundet", subjectResponses, ResultStatus.Success);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<GetSimpleSubjectResponse>>.Create(ex.Message, [], ResultStatus.Error);

            }
        }
    }
}
