using MediatR;
using Module.Shared.Models;
using Module.User.Application.Abstractions;

namespace Module.User.Application.Features.Bogus.Command;

public record GenerateUserLoginsCommand() : IRequest<Result<bool>>;

public class GenerateUserLoginsCommandHandler(IAccountLoginRepository accountLoginRepository) : IRequestHandler<GenerateUserLoginsCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(GenerateUserLoginsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}