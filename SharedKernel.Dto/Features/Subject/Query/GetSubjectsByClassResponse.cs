using System;
using System.Collections.Generic;

namespace SharedKernel.Dto.Features.Subject.Query
{
    public record GetSubjectsByClassResponse(
        string ClassName,
        IEnumerable<GetAllSubjectsResponse> Subjects
    );


}