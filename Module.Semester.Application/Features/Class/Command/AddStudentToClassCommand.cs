using MediatR;
using Module.Semester.Application.Abstractions;
using Module.Semester.Application.Features.Class.Command.Dto;
using Module.Shared.Abstractions;

namespace Module.Semester.Application.Features.Class.Command;

public record AddStudentToClassCommand(AddStudentToClassRequest AddStudentToClassRequest) : IRequest, ITransactionalCommand;

public class AddStudentToClassCommandHandler : IRequestHandler<AddStudentToClassCommand>
{
    private readonly IClassRepository _classRepository;

    public AddStudentToClassCommandHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }
    async Task IRequestHandler<AddStudentToClassCommand>.Handle(
        AddStudentToClassCommand request, CancellationToken cancellationToken)
    {
        // Load
        var classToUpdate = await _classRepository.GetClassByIdAsync(request.AddStudentToClassRequest.ClassId);
        var student = await _classRepository.GetUserByIdAsync(request.AddStudentToClassRequest.StudentId);
        
        // Act
        classToUpdate.AddStudent(student);
        
        // Save
        await _classRepository.AddUserToClassAsync();
    }
}