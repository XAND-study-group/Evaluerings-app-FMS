using MediatR;
using Module.Shared.Abstractions;
using Module.User.Application.Abstractions;
using SharedKernel.Dto.Features.User.Command;

namespace Module.User.Application.Features.User.Command
{
    public record CreateUserCommand(CreateUserRequest Request) : IRequest<Task>, ITransactionalCommand;
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Task>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Task> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            
            
            var userRequest = request.Request;

            // Create 
            var user = Domain.Entities.User.Create(userRequest.Firstname, userRequest.Lastname, userRequest.Email);

            // Do & Save 
            await _userRepository.CreateUserAsync(user);
            return Task.CompletedTask;
        }
    }
}
