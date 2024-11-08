using System.Xml.XPath;
using MediatR;
using Module.Authentication.Application.Abstractions.Repositories;
using Module.Authentication.Domain.DomainServices.Interfaces;
using Module.Authentication.Domain.Entity;
using Module.Shared.Domain.Models;

namespace Module.Authentication.Application.Features.SignUp.Commands;

public record AccountSignUpCommand(string Email, string Password) : IRequest<Result<bool>>;

public class AccountSignUpCommandHandler : IRequestHandler<AccountSignUpCommand, Result<bool>>
{
    private readonly IAccountLoginRepository _accountLoginRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AccountSignUpCommandHandler(IAccountLoginRepository accountLoginRepository, IPasswordHasher passwordHasher)
    {
        _accountLoginRepository = accountLoginRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<Result<bool>> Handle(AccountSignUpCommand request, CancellationToken cancellationToken)
    {
        // TODO: Use DTO request instead
        var exists = await _accountLoginRepository.DoesAccountLoginEmailExistAsync(request.Email);
        if (!exists)
            return Result<bool>.Create("Email already exists", false, ResultStatus.Error);

        var accountLogin = AccountLogin.Create(request.Email, request.Password, _passwordHasher);

        await _accountLoginRepository.CreateAccountLoginAsync(accountLogin);

        return Result<bool>.Create("Account created", false, ResultStatus.Success);
    }
}