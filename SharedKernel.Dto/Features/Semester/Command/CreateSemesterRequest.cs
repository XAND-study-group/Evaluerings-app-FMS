using Module.Semester.Domain.Enums;

namespace SharedKernel.Dto.Features.Semester.Command;

public record CreateSemesterRequest(
    string Name,
    int SemesterNumber,
    DateOnly StartDate,
    DateOnly EndDate,
    SchoolType School);