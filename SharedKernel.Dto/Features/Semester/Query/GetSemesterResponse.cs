﻿using Module.Semester.Domain.Enums;

namespace SharedKernel.Dto.Features.Semester.Query;

public record GetSemesterResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    int SemesterNumber,
    DateOnly StartDate,
    DateOnly EndDate,
    SchoolType School,
    IEnumerable<GetSemesterUserResponse> Responsibles);