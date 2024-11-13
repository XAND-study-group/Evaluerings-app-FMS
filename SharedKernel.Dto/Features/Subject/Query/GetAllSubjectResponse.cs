﻿using System;
using SharedKernel.Dto.Features.Lecture.Query;

namespace SharedKernel.Dto.Features.Subject.Query
{
    public record GetAllSubjectsResponse(
        Guid Id,
        string Name,
        string Description,
        IEnumerable<GetLectureResponse> Lectures);

}

