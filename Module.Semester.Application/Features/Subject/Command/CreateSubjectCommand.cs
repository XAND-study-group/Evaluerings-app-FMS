using MediatR;
using SharedKernel.Dto.Features.Subject.Command;

namespace Module.Semester.Application.Features.Subject.Command;

// ToDo: Changes response to status object
public record CreateSubjectCommand(CreateSubjectRequest Request) : IRequest<Task>;

public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Task>
{
    public async Task<Task> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}