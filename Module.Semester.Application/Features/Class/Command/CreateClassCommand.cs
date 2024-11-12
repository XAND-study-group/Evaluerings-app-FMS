using MediatR;
using Module.Semester.Application.Abstractions;
using Module.Shared.Abstractions;
using SharedKernel.Dto.Features.Class.Command;

namespace Module.Semester.Application.Features.Class.Command;

public record CreateClassCommand(
    CreateClassRequest CreateClassRequest) : IRequest, ITransactionalCommand;

internal class CreateClassCommandHandler : IRequestHandler<CreateClassCommand>
{
    private readonly IClassRepository _classRepository;

    public CreateClassCommandHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        // Load
        var semester = await _classRepository.GetSemesterById(request.CreateClassRequest.SemesterId);
        var otherClasses = await _classRepository.GetAllClassesAsync();

        // Do
        var classToCreate = semester.AddClass(request.CreateClassRequest.Name,
            request.CreateClassRequest.Description,
            request.CreateClassRequest.StudentCapacity,
            otherClasses);

        // Save
        await _classRepository.CreateClassAsync(classToCreate);
    }
}