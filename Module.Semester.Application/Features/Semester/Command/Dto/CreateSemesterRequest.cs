using Module.Semester.Domain.Enums;

namespace Module.Semester.Application.Features.Semester.Command.Dto;

public record CreateSemesterRequest(
    string Name,
    int SemesterNumber,
    DateOnly StartDate,
    DateOnly EndDate,
    SchoolType School);