using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Subject.Query;
using Module.Semester.Domain.Extension;
using Module.Semester.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Dto.Features.Subject.Query;
using SharedKernel.Models;

namespace Module.Semester.Infrastructure.Features.Subject
{
    public class GetAllSubjectsQueryHandler(SemesterDbContext _semesterDbContext) : IRequestHandler<GetAllSubjectsQuery, Result<IEnumerable<GetSimpleSubjectResponse>>>
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
