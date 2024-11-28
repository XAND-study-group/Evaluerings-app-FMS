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

        public void AddRefreshToken(string token, DateTime expirationDate)
            => _refreshTokens.Add(RefreshToken.Create(token, expirationDate));

        public void SetUserFirstname(string firstname)
            => Firstname = firstname;

        public void SetUserLastname(string lastname)
            => Lastname = lastname;

        public void SetUserEmail(string email)
            => Email = UserEmail.Create(email);

    }
}
