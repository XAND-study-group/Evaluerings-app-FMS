using MediatR;
using Module.Semester.Application.Abstractions;
using Module.Shared.Abstractions;
using SharedKernel.Dto.Features.Semester.Command;

namespace Module.Semester.Application.Features.Semester.Command;

public record AddResponsibleToSemesterCommand(AddResponsibleToSemesterRequest Request) : IRequest, ITransactionalCommand;

public class AddResponsibleToSemesterCommandHandler : IRequestHandler<AddResponsibleToSemesterCommand>
{
    private readonly ISemesterRepository _semesterRepository;

    public AddResponsibleToSemesterCommandHandler(ISemesterRepository semesterRepository)
    {
        _semesterRepository = semesterRepository;
    }
    async Task IRequestHandler<AddResponsibleToSemesterCommand>.Handle(AddResponsibleToSemesterCommand command, CancellationToken cancellationToken)
    {
        // Load
        var semester = await _semesterRepository.GetSemesterById(command.Request.SemesterId);
        var teacher = await _semesterRepository.GetUserById(command.Request.UserId);
        
        // Do
        semester.AddResponsible(teacher);
        
        // Save
        await _semesterRepository.AddResponsibleToSemester();
    }
}