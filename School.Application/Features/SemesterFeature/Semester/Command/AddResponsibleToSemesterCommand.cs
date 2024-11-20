using MediatR;
using School.Application.Abstractions.Semester;
using SharedKernel.Dto.Features.School.Semester.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Semester.Command;

public record AddResponsibleToSemesterCommand(AddResponsibleToSemesterRequest Request)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class AddResponsibleToSemesterCommandHandler : IRequestHandler<AddResponsibleToSemesterCommand, Result<bool>>
{
    private readonly ISemesterRepository _semesterRepository;

    public AddResponsibleToSemesterCommandHandler(ISemesterRepository semesterRepository)
    {
        _semesterRepository = semesterRepository;
    }

    async Task<Result<bool>> IRequestHandler<AddResponsibleToSemesterCommand, Result<bool>>.Handle(
        AddResponsibleToSemesterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var semester = await _semesterRepository.GetSemesterById(command.Request.SemesterId);
            var teacher = await _semesterRepository.GetUserById(command.Request.UserId);

            // Do
            semester.AddResponsible(teacher);

            // Save
            await _semesterRepository.AddResponsibleToSemester();

            return Result<bool>.Create("Ansvarlig tilføjet", true, ResultStatus.Added);
        }
        catch (ArgumentException e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}