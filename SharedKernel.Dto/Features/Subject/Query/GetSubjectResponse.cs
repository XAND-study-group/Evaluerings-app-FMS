using System;
using SharedKernel.Dto.Features.Lecture.Query;

namespace SharedKernel.Dto.Features.Subject.Query
{
    public record GetSubjectResponse(
        Guid Id,
        string Name,
        string Description,
        IEnumerable<GetLectureResponse> Lectures);
}