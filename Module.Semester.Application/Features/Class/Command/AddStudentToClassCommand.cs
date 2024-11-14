using MediatR;
using Module.Semester.Application.Abstractions;
using Module.Shared.Models;
using SharedKernel.Dto.Features.School.Class.Command;
using SharedKernel.Interfaces;

namespace Module.Semester.Application.Features.Class.Command;

public record AddStudentToClassCommand(AddStudentToClassRequest AddStudentToClassRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class AddStudentToClassCommandHandler : IRequestHandler<AddStudentToClassCommand, Result<bool>>
{
    private readonly IClassRepository _classRepository;

    public AddStudentToClassCommandHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    async Task<Result<bool>> IRequestHandler<AddStudentToClassCommand, Result<bool>>.Handle(
        AddStudentToClassCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var classToUpdate = await _classRepository.GetClassByIdAsync(request.AddStudentToClassRequest.ClassId);
            var student = await _classRepository.GetUserByIdAsync(request.AddStudentToClassRequest.StudentId);

            // Act
            classToUpdate.AddStudent(student);

            // Save
            await _classRepository.AddUserToClassAsync();
            
            return Result<bool>.Create("Elev tilføjet", false, ResultStatus.Added);
        }
        catch (ArgumentException e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}