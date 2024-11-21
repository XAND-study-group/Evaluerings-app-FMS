using School.Domain.Entities;
using SharedKernel.Dto.Features.School.Subject.Query;

namespace School.Domain.Extension
{
    public static class SubjectExtension
    {
        public static GetSimpleSubjectResponse MapToGetSimpleSubjectResponse(this Subject subject)=>
         new GetSimpleSubjectResponse(subject.Id, subject.Name, subject.Description, subject.Lectures.Select(l => l.MapToGetSimpleLectureResponse()));
        
        public static IEnumerable<GetSimpleSubjectResponse>MapToIEnumerableGetSubjectResponse(this IEnumerable<Subject> subjects) =>
         subjects.Select(s => s.MapToGetSimpleSubjectResponse());
    }
}
