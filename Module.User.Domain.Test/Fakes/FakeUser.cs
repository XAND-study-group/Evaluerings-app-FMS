using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.ValueObjects;

namespace Module.User.Domain.Test.Fakes
{
    public class FakeUser : Entities.User
    {
        public FakeUser(string email)
        {
            Email = UserEmail.Create(email, []);
        }

        public FakeUser()
        {
        }

        public void SetRefreshToken(string token, DateTime expirationDate)
            => RefreshToken = SharedKernel.ValueObjects.RefreshToken.Create(token, expirationDate);
        
        public void SetUserFirstname(string firstname)
            => Firstname = UserFirstname.Create(firstname);
        
        public void SetUserLastname(string lastname)
            => Lastname = UserLastname.Create(lastname);

        public void SetUserEmail(string email, IEnumerable<string> otherEmails)
            => Email = UserEmail.Create(email, otherEmails);

    }
}
