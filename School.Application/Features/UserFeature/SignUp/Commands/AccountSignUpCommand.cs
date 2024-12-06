using MediatR;
using School.Application.Abstractions.User;
using School.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Enums.Features.Authentication;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.SignUp.Commands;

public record AccountSignUpCommand(CreateAccountLoginRequest Request) : IRequest<Result<bool>>, ITransactionalCommand;

public class AccountSignUpCommandHandler(
    IUserRepository userRepository,
    IUserDomainService userDomainService,
    IAccountClaimRepository accountClaimRepository)
    : IRequestHandler<AccountSignUpCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AccountSignUpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var createRequest = request.Request;

            var exists = await userRepository.DoesUserEmailExistAsync(createRequest.Email);
            if (!exists)
                return Result<bool>.Create("Email already exists", false, ResultStatus.Error);
            
            var user = await Domain.Entities.User.CreateAsync(createRequest.Firstname, createRequest.Lastname,
                createRequest.Email,
                createRequest.Password, Role.User, userDomainService, accountClaimRepository);

            await userRepository.CreateUserAsync(user);

            return Result<bool>.Create("Account created", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}