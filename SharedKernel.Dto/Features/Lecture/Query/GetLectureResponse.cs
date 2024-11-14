﻿namespace SharedKernel.Dto.Features.Lecture.Query;

public record GetLectureResponse(
    Guid Id,
    byte[] RowVersion,
    string LectureTitle,
    string Description,
    TimeOnly FromTime,
    TimeOnly ToTime,
    DateOnly Date,
    string ClassRoom,
    IEnumerable<GetLectureUserResponse> Teachers);