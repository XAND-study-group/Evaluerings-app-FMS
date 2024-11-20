using System;
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Dto.Features.School.Lecture.Query;

namespace SharedKernel.Dto.Features.Subject.Query
{
    public record GetSimpleSubjectResponse(
            Guid Id,
            string Name,
            string Description,
            IEnumerable<GetSimpleLectureResponse> Lectures
        );

}