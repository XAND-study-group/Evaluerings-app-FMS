using Bogus;
using MediatR;
using School.Application.Abstractions.User;
using School.Domain.DomainServices.Interfaces;
using School.Domain.Entities;
using SharedKernel.Enums.Features.Authentication;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.Bogus.Command;

public record GenerateUserLoginsCommand() : IRequest<Result<bool>>;

public class GenerateUserLoginsCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<GenerateUserLoginsCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(GenerateUserLoginsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Role[] roles = [Role.User, Role.Teacher, Role.Admin];


            //TODO: add other users
            var accountLoginFake = new Faker<Domain.Entities.User>()
                .CustomInstantiator(f =>
                    Domain.Entities.User.Create(
                        f.Person.FirstName,
                        f.Person.LastName,
                        f.Person.Email, 
                        "Password123.", 
                        f.PickRandom(roles),
                        [],
                        passwordHasher)).UseSeed(420);

            var accounts = accountLoginFake.GenerateLazy(30);

            await userRepository.CreateUsersAsync(accounts);

            return Result<bool>.Create("Users have been created", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}