using MediatR;
using Module.User.Application.Abstractions;
using SharedKernel.Dto.Features.School.User.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.User.Application.Features.User.Command
{
    public record CreateUserCommand(CreateUserRequest Request) : IRequest<Result<bool>>, ITransactionalCommand;
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        async Task<Result<bool>> IRequestHandler<CreateUserCommand, Result<bool>>.Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Load
                var otherUsers = await _userRepository.GetAllUsers();
                var userRequest = request.Request;

                // Do 
                var user = Domain.Entities.User.Create(
                    userRequest.Firstname, 
                    userRequest.Lastname, 
                    userRequest.Email,
                    otherUsers);

                // Save 
                await _userRepository.CreateUserAsync(user);
                return Result<bool>.Create("Brugeren er blevet oprettet.", true, ResultStatus.Created);
            }
            catch (ArgumentException e)
            {
                return Result<bool>.Create(e.Message, true, ResultStatus.Created);
            }
        }
    }
}
