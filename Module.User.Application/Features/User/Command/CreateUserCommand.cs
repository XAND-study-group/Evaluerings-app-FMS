using MediatR;
using Module.User.Application.Abstractions;
using SharedKernel.Dto.Features.User.Command;
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

        public async Task<Result<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userRequest = request.Request;

                // Create 
                var user = Domain.Entities.User.Create(userRequest.Firstname, userRequest.Lastname, userRequest.Email);

                // Do & Save 
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
