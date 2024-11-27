using School.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace School.Domain.Test.Fakes.User
{
    public class FakeUser : Domain.Entities.User
    {
        public FakeUser(string email)
        {
            Email = UserEmail.Create(email);
        }

        public FakeUser()
        {
        }

        public FakeUser(string firstname, string lastname, string email)
        {
        }

        public void SetRefreshToken(string token, DateTime expirationDate)
            => RefreshToken = RefreshToken.Create(token, expirationDate);
        
        public void SetUserFirstname(string firstname)
            => Firstname = UserFirstname.Create(firstname);
        
        public void SetUserLastname(string lastname)
            => Lastname = UserLastname.Create(lastname);

        public void SetUserEmail(string email)
            => Email = UserEmail.Create(email);

    }
}
