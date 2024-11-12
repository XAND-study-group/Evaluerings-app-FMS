using Module.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Abstractions
{
    public interface IUserRepository
    {
        Task CreateUserAsync(Domain.Entities.User user);
    }
}
