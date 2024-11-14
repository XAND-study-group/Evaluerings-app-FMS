using MediatR;
using Module.User.Application.Abstractions;
using Module.User.Domain.DomainServices.Interfaces;
using Module.User.Domain.Entities;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Models;

namespace Module.User.Application.Features.SignUp.Commands;

public record AccountSignUpCommand(CreateAccountLoginRequest Request) : IRequest<Result<bool>>;

public class AccountSignUpCommandHandler(IAccountLoginRepository accountLoginRepository, IPasswordHasher passwordHasher)
    : IRequestHandler<AccountSignUpCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AccountSignUpCommand request, CancellationToken cancellationToken)
    {
        // TODO: Change to try/catch
        var createRequest = request.Request;
        
        var exists = await accountLoginRepository.DoesAccountLoginEmailExistAsync(createRequest.Email);
        if (!exists)
            return Result<bool>.Create("Email already exists", false, ResultStatus.Error);

        var user = Domain.Entities.User.Create(createRequest.Firstname, createRequest.Lastname, createRequest.Email);
        var accountLogin = AccountLogin.Create(createRequest.Email, createRequest.Password, user, passwordHasher);

        await accountLoginRepository.CreateAccountLoginAsync(accountLogin);

        return Result<bool>.Create("Account created", true, ResultStatus.Success);
    }
}