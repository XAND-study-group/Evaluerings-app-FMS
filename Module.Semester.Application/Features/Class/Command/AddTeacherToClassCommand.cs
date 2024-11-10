using MediatR;
using Module.Semester.Application.Abstractions;
using Module.Semester.Application.Features.Class.Command.Dto;
using Module.Shared.Abstractions;

namespace Module.Semester.Application.Features.Class.Command;

public record AddTeacherToClassCommand(AddTeacherToClassRequest AddTeacherToClassRequest) : IRequest, ITransactionalCommand;

public class AddTeacherToClassCommandHandler : IRequestHandler<AddTeacherToClassCommand>
{
    private readonly IClassRepository _classRepository;

    public AddTeacherToClassCommandHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }
    async Task IRequestHandler<AddTeacherToClassCommand>.Handle(
        AddTeacherToClassCommand request, CancellationToken cancellationToken)
    {
        // Load
        var classToUpdate = await _classRepository.GetClassByIdAsync(request.AddTeacherToClassRequest.ClassId);
        var teacher = await _classRepository.GetUserByIdAsync(request.AddTeacherToClassRequest.TeacherId);
        
        // Act
        classToUpdate.AddTeacher(teacher);
        
        // Save
        await _classRepository.AddUserToClassAsync();
    }
}