using MediatR;
using School.Application.Abstractions.User;
using School.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.School.User.Command;
using SharedKernel.Enums.Features.Authentication;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace School.Application.Features.UserFeature.User.Command;

public record CreateUserCommand(CreateUserRequest Request) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUserDomainService userDomainService,
    IAccountClaimRepository accountClaimRepository)
    : IRequestHandler<CreateUserCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateUserCommand, Result<bool>>.Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var userRequest = request.Request;

            // Do 
            var user = Domain.Entities.User.Create(
                userRequest.Firstname,
                userRequest.Lastname,
                userRequest.Email,
                userRequest.Password,
                Role.User,
                userDomainService,
                accountClaimRepository);

            // Save 
            await userRepository.CreateUserAsync(user);
            return Result<bool>.Create("Brugeren er blevet oprettet.", true, ResultStatus.Created);
        }
        catch (ArgumentException e)
        {
            return Result<bool>.Create(e.Message, true, ResultStatus.Created);
        }
    }
}