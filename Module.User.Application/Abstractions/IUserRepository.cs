using Module.User.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Abstractions
{
    public interface IUserRepository
    {

        #region User

        Task CreateUserAsync(Domain.Entity.User user);

        #endregion 
    }
}
