using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Domain.Test.Fakes
{
    public class FakeUser : Entities.User
    {
        public FakeUser(string firstname, string lastname, string email)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
        }


        public new void ValidateName(string name)
            => base.ValidateName(name);

        public new void ValidateEmail(string email)
            => base.ValidateEmail(email);

    }
}
