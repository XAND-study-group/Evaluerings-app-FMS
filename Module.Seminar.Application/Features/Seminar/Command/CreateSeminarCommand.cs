using MediatR;
using Module.Seminar.Application.Abstractions;
using Module.Seminar.Application.Features.Seminar.Command.Dto;
using Module.Shared.Abstractions;

namespace Module.Seminar.Application.Features.Seminar.Command;

public record CreateSeminarCommand(
    CreateSeminarRequest CreateSeminarRequest) : IRequest, ITransactionalCommand;

internal class CreateSeminarCommandHandler : IRequestHandler<CreateSeminarCommand>
{
    private readonly ISeminarRepository _seminarRepository;

    public CreateSeminarCommandHandler(ISeminarRepository seminarRepository)
    {
        _seminarRepository = seminarRepository;
    }

    public async Task Handle(CreateSeminarCommand request, CancellationToken cancellationToken)
    {
        // Load
        var otherSeminars = await _seminarRepository.GetAllSeminarsAsync();

        // Do
        var seminar = Domain.Entity.Seminar.Create(
            request.CreateSeminarRequest.Name,
            request.CreateSeminarRequest.Description,
            request.CreateSeminarRequest.StartDate,
            request.CreateSeminarRequest.EndDate,
            request.CreateSeminarRequest.StudentCapacity,
            otherSeminars);

        // Save
        await _seminarRepository.CreateSeminarAsync(seminar);
    }
}