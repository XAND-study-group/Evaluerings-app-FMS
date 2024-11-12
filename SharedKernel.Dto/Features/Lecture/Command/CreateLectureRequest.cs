﻿namespace SharedKernel.Dto.Features.Lecture.Command;

public record CreateLectureRequest(
    string LectureTitle,
    string Description,
    TimeOnly StartTime,
    TimeOnly EndTime,
    DateOnly Date,
    string ClassRoom,
    
    Guid SemesterId,
    Guid ClassId,
    Guid SubjectId);