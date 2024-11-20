using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Semester.Domain.Entities;
using SharedKernel.Dto.Features.Subject.Query;

namespace Module.Semester.Domain.Extension
{
    public static class SubjectExtension
    {
        public static GetSimpleSubjectResponse MapToGetSimpleSubjectResponse(this Subject subject)=>
         new GetSimpleSubjectResponse(subject.Id, subject.Name, subject.Description, subject.Lectures.Select(l => l.MapToGetSimpleLectureResponse()));
        
        public static IEnumerable<GetSimpleSubjectResponse>MapToIEnumerableGetSubjectResponse(this IEnumerable<Subject> subjects) =>
         subjects.Select(s => s.MapToGetSimpleSubjectResponse());
    }
}
