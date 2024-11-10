﻿using Module.Semester.Domain.Enums;

namespace Module.Semester.Application.Features.Semester.Query.Dto;

public record GetSemesterResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    int SemesterNumber,
    DateOnly StartDate,
    DateOnly EndDate,
    SchoolType School,
    IEnumerable<GetSemesterUserResponse> Responsibles);