using MediatR;
using Module.Seminar.Application.Abstractions;
using Module.Seminar.Application.Features.Seminar.Command.Dto;
using Module.Shared.Abstractions;

namespace Module.Seminar.Application.Features.Seminar.Command;

public record AddStudentToSeminarCommand(AddStudentToSeminarRequest AddStudentToSeminarRequest) : IRequest, ITransactionalCommand;

public class AddStudentToSeminarCommandHandler : IRequestHandler<AddStudentToSeminarCommand>
{
    private readonly ISeminarRepository _seminarRepository;

    public AddStudentToSeminarCommandHandler(ISeminarRepository seminarRepository)
    {
        _seminarRepository = seminarRepository;
    }
    async Task IRequestHandler<AddStudentToSeminarCommand>.Handle(
        AddStudentToSeminarCommand request, CancellationToken cancellationToken)
    {
        // Load
        var seminar = await _seminarRepository.GetSeminarByIdAsync(request.AddStudentToSeminarRequest.SeminarId);
        var student = await _seminarRepository.GetUserByIdAsync(request.AddStudentToSeminarRequest.StudentId);
        
        // Act
        seminar.AddStudent(student);
        
        // Save
        await _seminarRepository.AddUserToSeminarAsync(seminar);
    }
}