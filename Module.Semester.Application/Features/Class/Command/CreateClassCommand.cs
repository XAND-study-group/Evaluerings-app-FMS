using MediatR;
using Module.Semester.Application.Abstractions;
using Module.Semester.Application.Features.Class.Command.Dto;
using Module.Shared.Abstractions;

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
        var otherClasses = await _classRepository.GetAllClassesAsync();

        // Do
        var classToCreate = Module.Semester.Domain.Entity.Class.Create(
            request.CreateClassRequest.Name,
            request.CreateClassRequest.Description,
            request.CreateClassRequest.StudentCapacity,
            otherClasses);

        // Save
        await _classRepository.CreateClassAsync(classToCreate);
    }
}