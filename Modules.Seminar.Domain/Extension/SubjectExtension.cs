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
        public static GetSubjectResponse MapToGetSubjectResponse(this Subject subject)=>
         new GetSubjectResponse(subject.Id, subject.Name, subject.Description, subject.Lectures.Select(l => l.MapToGetLectureResponse()));
        
        public static IEnumerable<GetSubjectResponse>MapToIEnumerableGetSubjectResponse(this IEnumerable<Subject> subjects) =>
         subjects.Select(s => s.MapToGetSubjectResponse());
    }
}
