using Module.Seminar.Application.Features.Seminar.Query.Dto;
using Module.Seminar.Domain.Entity;

namespace Module.Seminar.Application.Features.Seminar.Query;

public record GetSeminarResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate,
    int StudentCapacity,
    IEnumerable<GetSeminarUserResponse> Teachers,
    IEnumerable<GetSeminarUserResponse> Students,
    IEnumerable<GetSeminarSubjectResponse> Subjects);