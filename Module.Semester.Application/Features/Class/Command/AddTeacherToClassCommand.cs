using MediatR;
using Module.Semester.Application.Abstractions;
using SharedKernel.Dto.Features.Class.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Class.Command;

public record AddTeacherToClassCommand(AddTeacherToClassRequest AddTeacherToClassRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class AddTeacherToClassCommandHandler : IRequestHandler<AddTeacherToClassCommand, Result<bool>>
{
    private readonly IClassRepository _classRepository;

    public AddTeacherToClassCommandHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    async Task<Result<bool>> IRequestHandler<AddTeacherToClassCommand, Result<bool>>.Handle(
        AddTeacherToClassCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var classToUpdate = await _classRepository.GetClassByIdAsync(request.AddTeacherToClassRequest.ClassId);
            var teacher = await _classRepository.GetUserByIdAsync(request.AddTeacherToClassRequest.TeacherId);

            // Act
            classToUpdate.AddTeacher(teacher);

            // Save
            await _classRepository.AddUserToClassAsync();

            return Result<bool>.Create("Lærer tilføjet", true, ResultStatus.Added);
        }
        catch (ArgumentException e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}