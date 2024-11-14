using MediatR;
using Module.Semester.Application.Abstractions;
using SharedKernel.Dto.Features.Class.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Class.Command;

public record CreateClassCommand(
    CreateClassRequest CreateClassRequest) : IRequest<Result<bool>>, ITransactionalCommand;

internal class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Result<bool>>
{
    private readonly IClassRepository _classRepository;

    public CreateClassCommandHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<Result<bool>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        try
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

            return Result<bool>.Create("Klasse oprettet", true, ResultStatus.Created);
        }
        catch (ArgumentException e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
        catch (InvalidOperationException ie)
        {
            return Result<bool>.Create(ie.Message, false, ResultStatus.Error);
        }
    }
}