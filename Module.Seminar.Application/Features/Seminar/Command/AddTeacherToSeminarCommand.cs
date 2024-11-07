using MediatR;
using Module.Seminar.Application.Abstractions;
using Module.Seminar.Application.Features.Seminar.Command.Dto;
using Module.Shared.Abstractions;

namespace Module.Seminar.Application.Features.Seminar.Command;

public record AddTeacherToSeminarCommand(AddTeacherToSeminarRequest AddTeacherToSeminarRequest) : IRequest, ITransactionalCommand;

public class AddTeacherToSeminarCommandHandler : IRequestHandler<AddTeacherToSeminarCommand>
{
    private readonly ISeminarRepository _seminarRepository;

    public AddTeacherToSeminarCommandHandler(ISeminarRepository seminarRepository)
    {
        _seminarRepository = seminarRepository;
    }
    async Task IRequestHandler<AddTeacherToSeminarCommand>.Handle(
        AddTeacherToSeminarCommand request, CancellationToken cancellationToken)
    {
        // Load
        var seminar = await _seminarRepository.GetSeminarByIdAsync(request.AddTeacherToSeminarRequest.SeminarId);
        var teacher = await _seminarRepository.GetUserByIdAsync(request.AddTeacherToSeminarRequest.TeacherId);
        
        // Act
        seminar.AddTeacher(teacher);
        
        // Save
        await _seminarRepository.AddUserToSeminarAsync(seminar);
    }
}