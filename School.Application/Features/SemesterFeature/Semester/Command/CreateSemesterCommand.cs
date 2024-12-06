using MediatR;
using School.Application.Abstractions.Semester;
using SharedKernel.Dto.Features.School.Semester.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Semester.Command;

public record CreateSemesterCommand(CreateSemesterRequest CreateSemesterRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateSemesterCommandHandler : IRequestHandler<CreateSemesterCommand, Result<bool>>
{
    private readonly ISemesterRepository _semesterRepository;

    public CreateSemesterCommandHandler(ISemesterRepository semesterRepository)
    {
        _semesterRepository = semesterRepository;
    }

    async Task<Result<bool>> IRequestHandler<CreateSemesterCommand, Result<bool>>.Handle(CreateSemesterCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var otherSemesters = await _semesterRepository.GetAllSemesters();
            var createSemesterRequest = request.CreateSemesterRequest;

            // Do
            var semester = Domain.Entities.Semester.Create(
                createSemesterRequest.Name,
                createSemesterRequest.SemesterNumber,
                createSemesterRequest.StartDate,
                createSemesterRequest.EndDate,
                createSemesterRequest.School,
                otherSemesters);

            // Save
            await _semesterRepository.CreateSemesterAsync(semester);

            return Result<bool>.Create("Semester oprettet", true, ResultStatus.Created);
        }
        catch (ArgumentException e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}