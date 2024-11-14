using MediatR;
using Module.Shared.Models;
using Module.User.Application.Abstractions;
using SharedKernel.Dto.Features.School.Authentication.Command;

namespace Module.User.Application.Features.AccountClaim.Command;

public record AddClaimToUserCommand(AddClaimToUserRequest Request) : IRequest<Result<bool>>;

public class AddClaimToUserCommandHandler(IUserRepository userRepository, IAccountClaimRepository accountClaimRepository) : IRequestHandler<AddClaimToUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AddClaimToUserCommand request, CancellationToken cancellationToken)
    {
        // TODO: Change to try/catch
        var addClaimRequest = request.Request;

        var user = await userRepository.GetUserByIdAsync(addClaimRequest.UserId);
        var claim = Domain.Entities.AccountClaim.Create(addClaimRequest.ClaimName, addClaimRequest.ClaimValue);
        user.AddAccountClaim(claim);
        await accountClaimRepository.AddClaimToUserAsync(claim);

        return Result<bool>.Create("Rettighed er blevet tilføjet til brugeren", true, ResultStatus.Success);
    }
}