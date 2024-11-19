using System;
using SharedKernel.Dto.Features.Lecture.Query;

namespace SharedKernel.Dto.Features.Subject.Query
{
    public record GetSimpleSubjectResponse(
            Guid Id,
            string Name,
            string Description,
            IEnumerable<GetSimpleLectureResponse> Lectures
        );

}